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
using models = Splitwise.Models;

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

            models.Friendship friend = await proxy.GetFriend(friendshipId);

            viewModel = new FriendViewModel(friend);

            this.DataContext = viewModel;
        }

        private void NavigateToExpense(object sender, System.Windows.Input.GestureEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;

            if (textBlock != null)
            {
                models.Expense expense = textBlock.DataContext as models.Expense;

                if (expense != null)
                {
                    NavigationService.Navigate(new Uri(string.Format("/Views/Expense.xaml?expenseId={0}", expense.Id), UriKind.Relative));
                }
            }
        }
    }
}