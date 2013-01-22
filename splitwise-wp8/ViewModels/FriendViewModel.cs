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
        private ObservableCollection<Expense> _expenses = new ObservableCollection<Expense>();
        private SplitwiseProxy proxy = SplitwiseProxy.GetProxyInstance();

        public FriendViewModel(Friendship friend)
        {
            this.Friendship = friend;
            LoadExpenses(friend);
        }

        public ObservableCollection<Expense> Expenses
        {
            get { return _expenses; }
        }

        public Friendship Friendship { get; set; }

        private async void LoadExpenses(Friendship friend)
        {
            IEnumerable<Expense> expenses = await proxy.GetExpenses(friend.Id);

            foreach (var expense in expenses)
            {
                _expenses.Add(expense);
            }
        }
    }
}
