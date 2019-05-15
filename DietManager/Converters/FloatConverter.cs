using System;
using System.Globalization;
using System.Windows.Data;

namespace DietManager.Converters
{
    public class FloatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float fValue;
            return float.TryParse(value.ToString(), out fValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
