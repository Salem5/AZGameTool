using AZGameToolTry1.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace AZGameToolTry1.Converter
{
    public class BooleanToVisibilityConverter : MarkupExtension,
        IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == typeof(Boolean))
            {
                var check = (bool)value;
                if (check)
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static BooleanToVisibilityConverter _converter = null;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter = _converter ?? new BooleanToVisibilityConverter();
        }
    }
}