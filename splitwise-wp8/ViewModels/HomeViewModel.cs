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
        private SplitwiseProxy proxy = SplitwiseProxy.GetProxyInstance();

        public HomeViewModel()
        {
            LoadFriends();
        }

        private async void LoadFriends()
        {
            IEnumerable<User> friends = await proxy.GetFriends();

            foreach (var friend in friends)
            {
                _friends.Add(friend);
            }
        }

        public ObservableCollection<User> Friends
        {
            get
            {
                return _friends;
            }
        }
    }
}
