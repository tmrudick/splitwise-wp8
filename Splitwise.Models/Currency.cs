using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Splitwise.Models
{
    public class Currency
    {
        public string CountryCode { get; set; }
        public string CurrencyCode { get; set; }
        public string NegativeFormat { get; set; }
        public string Format { get; set; }
        public string Unit { get; set; }
        public int Precision { get; set; }
    }

    public class CurrencyWrapper
    {
        public List<Currency> Currencies { get; set; } 
    }
}
