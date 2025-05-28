using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WPF_Projekt
{
    public class ThumbColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Console.WriteLine($"ThumbColorConverter called: values = [{string.Join(",", values)}]");
            if (values.Length != 3 ||
                !(values[0] is double value) ||
                !(values[1] is double minimum) ||
                !(values[2] is double maximum))
                return Brushes.Gray;

            double percent = (value - minimum) / Math.Max((maximum - minimum), 1);

            // Farbverlauf von Lila (unten) zu Schwarz (oben)
            byte r = (byte)(128 * (1 - percent));
            byte g = (byte)(0 * (1 - percent));
            byte b = (byte)(128 * (1 - percent));

            return new SolidColorBrush(Color.FromRgb(r, g, b));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
