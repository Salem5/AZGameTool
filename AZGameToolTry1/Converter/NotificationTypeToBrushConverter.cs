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
    public class NotificationTypeToBrushConverter : MarkupExtension,
        IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            NotificationKind? valueEnum = value as NotificationKind?;
            string[] brushParameters = ((String)parameter).Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            return Application.Current.FindResource(brushParameters[Math.Min(((int?)valueEnum ?? 0),brushParameters.Length - 1)]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        
        private static NotificationTypeToBrushConverter _converter = null;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter = _converter ?? new NotificationTypeToBrushConverter();
        }
    }
}
