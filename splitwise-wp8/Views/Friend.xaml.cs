using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Splitwise.ViewModels;
using Splitwise.Models;

namespace Splitwise.Views
{
    public partial class Friend : PhoneApplicationPage
    {
        private FriendViewModel viewModel;
        private SplitwiseProxy proxy = SplitwiseProxy.GetProxyInstance();

        public Friend()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            long friendshipId = long.Parse(NavigationContext.QueryString["friendshipId"]);

            Friendship friend = await proxy.GetFriend(friendshipId);

            viewModel = new FriendViewModel(friend);

            this.DataContext = viewModel;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Helpers.BackStackHelpers.ShouldHideFromBackStack(NavigationContext.QueryString))
            {
                NavigationService.RemoveBackEntry();
            }
        }
    }
}