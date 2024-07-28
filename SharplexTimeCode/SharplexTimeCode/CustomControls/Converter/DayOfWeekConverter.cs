using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace SharplexTimeCode.CustomControls.Converter;

public class DayOfWeekConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is DayOfWeek dayOfWeek)
        {
            var dayOfWeekString = dayOfWeek.ToString();
            return dayOfWeekString.Length > 3 ? dayOfWeekString.Substring(0, 3).ToUpper(culture) : dayOfWeekString;
        }

        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}