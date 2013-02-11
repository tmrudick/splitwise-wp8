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
using Splitwise.Utility;

namespace Splitwise.Views
{
    public partial class NewExpense : PhoneApplicationPage
    {
        private NewExpenseViewModel ViewModel;

        public NewExpense()
        {
            InitializeComponent();

            this.ViewModel = new NewExpenseViewModel();
            this.DataContext = this.ViewModel;
        }

        private async void Save_Click(object sender, EventArgs e)
        {
            string description = DescriptionTextBox.Text;
            double amount = double.Parse(AmountTextBox.Text);

            IEnumerable<User> splitees = FriendSelector.SelectedItems.ToIEnumerable<User>();

            Expense expense = await ViewModel.Save(description, amount, splitees);

            if (expense != null)
            {
                NavigationService.Navigate(new Uri("/Views/Home.xaml?HideFromBackStack=true", UriKind.Relative));
            }
            else
            {
                ErrorMessageTextBox.Text = "Some kind of error happened.";
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
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