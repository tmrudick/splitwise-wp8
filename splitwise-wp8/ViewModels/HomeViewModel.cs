using Splitwise.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.ViewModels
{
    public class HomeViewModel
    {
        private ObservableCollection<Friendship> _friends = new ObservableCollection<Friendship>();
        private ObservableCollection<Expense> _expenses = new ObservableCollection<Expense>();
        private SplitwiseProxy proxy = SplitwiseProxy.GetProxyInstance();

        public HomeViewModel()
        {
            LoadFriendships();
            LoadExpenses();
        }

        private async void LoadFriendships()
        {
            IEnumerable<Friendship> friends = await proxy.GetFriends();

            foreach (var friend in friends)
            {
                _friends.Add(friend);
            }
        }

        private async void LoadExpenses()
        {
            IEnumerable<Expense> expenses = await proxy.GetExpenses();

            foreach (var expense in expenses)
            {
                _expenses.Add(expense);
            }
        }

        public ObservableCollection<Friendship> Friendships
        {
            get
            {
                return _friends;
            }
        }

        public ObservableCollection<Expense> Expenses
        {
            get
            {
                return _expenses;
            }
        }
    }
}
