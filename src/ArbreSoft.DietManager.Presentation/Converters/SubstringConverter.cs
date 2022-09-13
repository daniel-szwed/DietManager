    using System;
using System.Globalization;
using System.Windows.Data;

namespace ArbreSoft.DietManager.Presentation.Converters
{
    public class SubstringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string valueToConvert
                && parameter is string maxLengthString
                && int.TryParse(maxLengthString, out var maxLength)
                && valueToConvert.Length > maxLength)
            {
                return valueToConvert.Substring(0, maxLength) + "...";
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
    }
}
