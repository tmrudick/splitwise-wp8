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
    }
}