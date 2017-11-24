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
    public class NotificationTypeToPackIconConverter : MarkupExtension, IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            NotificationKind? valueEnum = value as NotificationKind?;
        
            switch (valueEnum)
            {
                case NotificationKind.Error:
                    return "AlertOctagon";
                case NotificationKind.Warning:
                    return "Alert";                    
                case NotificationKind.Info:
                    return "Information";
                case NotificationKind.Other:
                default:
                    return "HelpCircle";
            }            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        
        private static NotificationTypeToPackIconConverter _converter = null;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter = _converter ?? new NotificationTypeToPackIconConverter();
        }
    }
}
