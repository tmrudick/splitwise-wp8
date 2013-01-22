using Splitwise.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.ViewModels
{
    public class FriendViewModel
    {
        private ObservableCollection<Expense> _expenses;
        private SplitwiseProxy proxy = SplitwiseProxy.GetProxyInstance();

        public FriendViewModel(int friendshipId)
        {
            LoadExpenses(friendshipId);
        }

        public ObservableCollection<Expense> Expenses
        {
            get { return _expenses; }
        }

        private async void LoadExpenses(int friendshipId)
        {
            IEnumerable<Expense> expenses = await proxy.GetExpenses(friendshipId);

            foreach (var expense in expenses)
            {
                _expenses.Add(expense);
            }
        }
    }
}
