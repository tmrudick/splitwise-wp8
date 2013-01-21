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
        private ObservableCollection<User> _friends = new ObservableCollection<User>();
        private ObservableCollection<Expense> _expenses = new ObservableCollection<Expense>();
        private SplitwiseProxy proxy = SplitwiseProxy.GetProxyInstance();

        public HomeViewModel()
        {
            LoadFriends();
            LoadExpenses();
        }

        private async void LoadFriends()
        {
            IEnumerable<User> friends = await proxy.GetFriends();

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

        public ObservableCollection<User> Friends
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
