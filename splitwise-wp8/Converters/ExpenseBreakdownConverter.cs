using Splitwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Splitwise.Converters
{
    public class ExpenseBreakdownConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Payment payment = (Payment)value;
            
            decimal netBalance = decimal.Parse(payment.NetBalance);

            string cashDirection = netBalance < 0 ? "owes" : "gets back";

            return string.Format("{0} {1}", cashDirection, Math.Abs(netBalance));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
