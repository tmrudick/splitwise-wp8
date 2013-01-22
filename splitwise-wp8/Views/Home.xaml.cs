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
    public partial class Home : PhoneApplicationPage
    {
        private HomeViewModel ViewModel;

        public Home()
        {
            InitializeComponent();

            this.ViewModel = new HomeViewModel();
            this.DataContext = this.ViewModel;
        }

        private void Friendships_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Friendship friendship = ((FrameworkElement)e.OriginalSource).DataContext as Friendship;

            if (friendship != null)
            {
                NavigationService.Navigate(new Uri("/Views/Friend.xaml?friendshipId=" + friendship.Id, UriKind.Relative));
            }
        }
    }
}