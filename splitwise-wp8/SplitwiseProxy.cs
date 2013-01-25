using RestSharp;
using Splitwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Splitwise.Utility;

namespace Splitwise
{
    public class SplitwiseProxy
    {
        private static SplitwiseProxy proxy;

        private const string baseUrl = "https://secure.splitwise.com/api/v2.1";
        private RestClient client;
        private User currentUser;

        protected SplitwiseProxy()
        {
            client = new RestClient(baseUrl);
            // TODO: Figure out why this isn't working after the OAuth flow completes
            // client.AddDefaultUrlSegment("version", "v2.1");
        }

        public RestClient RestClient
        {
            get { return this.client; }
        }

        public User CurrentUser
        {
            get { return this.currentUser; }
        }

        public async Task<User> LoadCurrentUser()
        {
            var request = new RestRequest("get_current_user", Method.GET);
            UserWrapper wrapper = await this.client.ExecuteRequestAsync<UserWrapper>(request);

            this.currentUser = wrapper.User;

            return this.currentUser;
        }

        public async Task<IEnumerable<Expense>> GetExpenses()
        {
            return await GetExpenses(null);
        }

        public async Task<IEnumerable<Expense>> GetExpenses(long? friendshipId)
        {
            var request = new RestRequest("get_expenses", Method.GET);

            if (friendshipId != null)
            {
                request.AddParameter("friendship_id", friendshipId);
            }

            ExpenseWrapper wrapper = await this.client.ExecuteRequestAsync<ExpenseWrapper>(request);

            return wrapper.Expenses;
        }

        public async Task<IEnumerable<Friendship>> GetFriends()
        {
            var request = new RestRequest("get_friendships", Method.GET);
            FriendshipsWrapper wrapper = await this.client.ExecuteRequestAsync<FriendshipsWrapper>(request);

            wrapper.Friendships.ToList().ForEach(friendship => friendship.Self = this.CurrentUser);

            return wrapper.Friendships;
        }

        public async Task<Friendship> GetFriend(long friendshipId)
        {
            var request = new RestRequest("get_friendship/" + friendshipId, Method.GET);
            FriendshipWrapper wrapper = await this.client.ExecuteRequestAsync<FriendshipWrapper>(request);

            wrapper.Friendship.Self = this.CurrentUser;

            return wrapper.Friendship;
        }

        public async Task<Expense> CreateExpense(Expense expense)
        {
            var request = new RestRequest("create_expense", Method.POST);
            // TODO: Add shared expenses POST format.
            // http://dev.splitwise.com/dokuwiki/doku.php?id=create_expense
            request.AddBody(expense);

            Expense result = await this.client.ExecuteRequestAsync<Expense>(request);

            return result;
        }

        public static SplitwiseProxy GetProxyInstance()
        {
            if (proxy == null)
            {
                proxy = new SplitwiseProxy();
            }

            return proxy;
        }
    }

    public class OAuthCredentials
    {
        public string OAuthToken { get; set; }
        public string OAuthTokenSecret { get; set; }
        public string OAuthVerifier { get; set; }
    }
}
