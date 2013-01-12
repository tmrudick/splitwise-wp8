using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Splitwise.Models
{
    // TODO: This class might not be correct name
    public class Payment
    {
        public User User { get; set; }
        public string PaidShare { get; set; }
        public string OwedShare { get; set; }
        public string NetBalance { get; set; }
    }
}
