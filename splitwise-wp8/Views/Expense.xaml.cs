using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using models = Splitwise.Models;

namespace Splitwise.Views
{
    public partial class Expense : PhoneApplicationPage
    {
        private SplitwiseProxy proxy = SplitwiseProxy.GetProxyInstance();

        public Expense()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            long expenseId = long.Parse(NavigationContext.QueryString["expenseId"]);

            models.Expense expense = await proxy.GetExpense(expenseId);

            this.DataContext = expense;
        }
    }
}