using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Splitwise.Models
{
    public class Expense
    {
        public long Id { get; set; }
    }

    public class ExpenseWrapper
    {
        public List<Expense> Expenses { get; set; }
    }
}
