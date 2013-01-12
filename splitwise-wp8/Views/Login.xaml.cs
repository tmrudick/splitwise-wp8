using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RestSharp;
using RestSharp.Authenticators;
using Splitwise.Utility;

namespace splitwise_wp8.Views
{
    public partial class Login : PhoneApplicationPage
    {
        private const string consumerKey = "YwXp1fv6yiaRfpuCXi8dQCETqh81qTtJiH5tWbDb";
        private const string consumerSecret = "gkKrT7o2uF8iSF9rli0JmuDwWbYA7JvWrRsCKIDk";
        private const string baseUrl = "https://secure.splitwise.com/api/{version}";
        private const string authorizeUrl = "https://secure.splitwise.com/authorize";

        private string oauth_token, oauth_token_secret;

        private RestClient client;

        public Login()
        {
            InitializeComponent();

            client = new RestClient(baseUrl);
            client.AddDefaultUrlSegment("version", "v2.1");
        }

        private async void Login_Loaded(object sender, RoutedEventArgs e)
        {
            // Start the oAuth flow
            client.Authenticator = OAuth1Authenticator.ForRequestToken(consumerKey, consumerSecret);

            var request = new RestRequest("get_request_token", Method.POST);
            var response = await client.ExecuteRequestAsync(request);

            var qs = HttpHelpers.ParseQueryString(response.Content);
            oauth_token = qs["oauth_token"];
            oauth_token_secret = qs["oauth_token_secret"];

            var url = new Uri(string.Format("{0}?oauth_token={1}", authorizeUrl, oauth_token));
            LoginBrowser.Navigate(url);
        }

        private async void LoginBrowser_Navigating(object sender, NavigatingEventArgs e)
        {
            if (e.Uri.AbsoluteUri.Contains("oauth.tomrudick.com"))
            {
                var qs = HttpHelpers.ParseQueryString(e.Uri.Query);

                var verifier = qs["oauth_verifier"];
                var request = new RestRequest("get_access_token", Method.POST);
                client.Authenticator = OAuth1Authenticator.ForAccessToken(consumerKey, consumerSecret, this.oauth_token, this.oauth_token_secret, verifier);

                var response = await client.ExecuteRequestAsync(request);
                qs = HttpHelpers.ParseQueryString(response.Content);

                this.oauth_token = qs["oauth_token"];
                this.oauth_token_secret = qs["oauth_token_secret"];

                client.Authenticator = OAuth1Authenticator.ForProtectedResource(
                consumerKey, consumerSecret, this.oauth_token, this.oauth_token_secret
            );

                request = new RestRequest("get_expenses", Method.GET);
                response = await client.ExecuteRequestAsync(request);

                var a = 2;
            }
        }

        /*
         *   
            var verifier = "K83STEYYCfc1ncSXMvXO"; // <-- Breakpoint here (set verifier in debugger)
            request = new RestRequest("get_access_token", Method.POST);
            client.Authenticator = OAuth1Authenticator.ForAccessToken(
                consumerKey, consumerSecret, oauth_token, oauth_token_secret, verifier
            );
            response = client.Execute(request);

            //Assert.NotNull(response);
            //Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            qs = HttpUtility.ParseQueryString(response.Content);
            oauth_token = qs["oauth_token"];
            oauth_token_secret = qs["oauth_token_secret"];
            //Assert.NotNull(oauth_token);
            //Assert.NotNull(oauth_token_secret);

            request = new RestRequest("account/verify_credentials.xml");
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(
                consumerKey, consumerSecret, oauth_token, oauth_token_secret
            );

            response = client.Execute(request);
*/
    }
}