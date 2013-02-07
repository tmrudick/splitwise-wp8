using Splitwise.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Splitwise.Utility;

namespace Splitwise.ViewModels
{
    public class FriendViewModel
    {
        private SplitwiseProxy proxy = SplitwiseProxy.GetProxyInstance();

        public FriendViewModel(Friendship friend)
        {
            Expenses = new ObservableCollection<Expense>();

            this.Friendship = friend;
            LoadExpenses(friend);
        }

        public ObservableCollection<Expense> Expenses { get; private set; }

        public Friendship Friendship { get; set; }

        private async void LoadExpenses(Friendship friend)
        {
            IEnumerable<Expense> expenses = await proxy.GetExpenses(friend.Id);
            Expenses.AddRange(expenses);
        }
    }
}
