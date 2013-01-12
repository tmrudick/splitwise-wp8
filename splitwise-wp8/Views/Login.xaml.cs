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
using Splitwise;
using System.IO.IsolatedStorage;

namespace Splitwise.Views
{
    public partial class Login : PhoneApplicationPage
    {
        private const string consumerKey = "YwXp1fv6yiaRfpuCXi8dQCETqh81qTtJiH5tWbDb";
        private const string consumerSecret = "gkKrT7o2uF8iSF9rli0JmuDwWbYA7JvWrRsCKIDk";
        private const string authorizeUrl = "https://secure.splitwise.com/authorize";

        private string oauth_token, oauth_token_secret;

        private RestClient client;
        private SplitwiseProxy proxy;

        private IsolatedStorageSettings storage = System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings;

        public Login()
        {
            InitializeComponent();

            proxy = SplitwiseProxy.GetProxyInstance();

            client = proxy.RestClient;
        }

        private async void Login_Loaded(object sender, RoutedEventArgs e)
        {

            // Check isolated storage for existing OAuth information
            OAuthCredentials credentials;

            storage.TryGetValue("credentials", out credentials);

            if (credentials != null)
            {
                CompleteLogin(credentials);
            }
            else
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

                OAuthCredentials credentials = new OAuthCredentials() {
                    OAuthToken = this.oauth_token,
                    OAuthTokenSecret = this.oauth_token_secret,
                    OAuthVerifier = verifier
                };

                // Save to isolated storage
                storage["credentials"] = credentials;
                storage.Save();

                CompleteLogin(credentials);
            }
        }

        private async void CompleteLogin(OAuthCredentials credentials)
        {
            // Set up the authenticator on the client object
            this.client.Authenticator = OAuth1Authenticator.ForProtectedResource(
                consumerKey, consumerSecret, credentials.OAuthToken, credentials.OAuthTokenSecret
            );

            await this.proxy.GetCurrentUser();

            // Navigate to the MainPage.xaml file
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}