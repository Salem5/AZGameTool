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
    public class StringNullEmptyToVisibilityConverter : MarkupExtension, 
        IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool.TryParse((string)parameter, out bool negation);


            string stringValue = value as String;
            if (String.IsNullOrEmpty(stringValue))
            {
                return negation ? Visibility.Collapsed : Visibility.Visible;
            }
            else
            {
                return negation ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility? visValue = value as Visibility?;
            return visValue.ToString();
        }

        private static StringNullEmptyToVisibilityConverter _converter = null;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter = _converter ?? new StringNullEmptyToVisibilityConverter();
        }
    }
}
