using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Splitwise.Models
{
    public class Expense
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Cost { get; set; }

        public User CreatedBy { get; set; }
        public List<Payment> Users { get; set; }

        public Category Category { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public class ExpenseWrapper
    {
        public List<Expense> Expenses { get; set; }
    }
}
