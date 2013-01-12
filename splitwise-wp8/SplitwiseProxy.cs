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

        public async Task<IEnumerable<Expense>> GetExpenses()
        {
            var request = new RestRequest("get_expenses", Method.GET);
            ExpenseWrapper wrapper = await proxy.RestClient.ExecuteRequestAsync<ExpenseWrapper>(request);

            return wrapper.Expenses;
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
