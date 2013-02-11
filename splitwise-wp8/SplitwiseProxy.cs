using RestSharp;
using Splitwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Splitwise.Utility;
using Newtonsoft.Json.Linq;

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

        public async Task<IEnumerable<Expense>> GetExpenses(long? friendshipId, bool showDeleted = false)
        {
            var request = new RestRequest("get_expenses", Method.GET);

            if (friendshipId != null)
            {
                request.AddParameter("friendship_id", friendshipId);
            }

            ExpenseWrapper wrapper = await this.client.ExecuteRequestAsync<ExpenseWrapper>(request);

            if (showDeleted)
            {
                return wrapper.Expenses;
            }
            else
            {
                return wrapper.Expenses.Where(exp => exp.DeletedAt == null);
            }
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

            JObject payload = new JObject();
            payload["cost"] = expense.Cost;
            payload["payment"] = false;
            payload["description"] = expense.Description;

            for (int i = 0; i < expense.Users.Count; i++)
            {
                Payment payment = expense.Users[i];
                payload.AddIfNotDefault("users__" + i + "__user_id", payment.User.Id);
                payload.AddIfNotDefault("users__" + i + "__fist_name", payment.User.FirstName);
                payload.AddIfNotDefault("users__" + i + "__last_name", payment.User.LastName);
                payload.AddIfNotDefault("users__" + i + "__email", payment.User.Email);
                payload.AddIfNotDefault("users__" + i + "__paid_share", payment.PaidShare);
                payload.AddIfNotDefault("users__" + i + "__owed_share", payment.OwedShare);
            }
 
            var str = payload.ToString();
            request.AddParameter("application/json", str, ParameterType.RequestBody);

            ExpenseWrapper result = await this.client.ExecuteRequestAsync<ExpenseWrapper>(request);
            if (result.Expenses.Count == 1)
            {
                // Success
                return result.Expenses[0];
            } 

            return null;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var request = new RestRequest("get_categories", Method.GET);
            CategoriesWrapper wrapper = await this.client.ExecuteRequestAsync<CategoriesWrapper>(request);

            return wrapper.Categories;
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
