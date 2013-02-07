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
    public class NewExpenseViewModel
    {
        private SplitwiseProxy proxy = SplitwiseProxy.GetProxyInstance();

        public NewExpenseViewModel()
        {
            Friends = new ObservableCollection<User>();
            LoadFriends();
        }

        public async Task<Expense> Save(string description, double cost, IEnumerable<User> splitees)
        {
            Expense expense = new Expense();
            expense.Cost = cost.ToString();
            expense.Description = description;
            expense.Users = new List<Payment>();

            // Add your friends to the payment
            foreach (User user in splitees)
            {
                Payment payment = new Payment();

                payment.OwedShare = Math.Round((cost / (splitees.Count() + 1)), 2).ToString(); // Split the cost evenly (+1 because we need to count the current user)
                payment.PaidShare = "0.0";
                payment.User = user;

                expense.Users.Add(payment);
            }

            // Add you
            double remainingShare = cost - expense.Users.Select(payment => double.Parse(payment.OwedShare)).Aggregate((sum, next) => sum + next);
            expense.Users.Add(new Payment() { OwedShare = remainingShare.ToString(), PaidShare = expense.Cost, User = proxy.CurrentUser });

            Expense savedExpense = await proxy.CreateExpense(expense);

            return savedExpense;
        }

        private async void LoadFriends()
        {
            IEnumerable<Friendship> friendships = await proxy.GetFriends();
            IEnumerable<User> friends = friendships.Select(f => f.Friend);

            Friends.AddRange(friends);
        }

        public ObservableCollection<User> Friends { get; private set; }
    }
}
