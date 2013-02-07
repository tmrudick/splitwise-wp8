using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Splitwise.Converters
{
    public class DebtColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal amount = decimal.Parse(value.ToString());

            SolidColorBrush brush = new SolidColorBrush();

            if (amount < 0)
            {
                brush.Color = Colors.Red;
            }
            else if (amount > 0)
            {
                brush.Color = Colors.Green;
            }
            else
            {
                brush.Color = Colors.White;
            }

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
