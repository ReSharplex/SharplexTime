using System;
using System.Globalization;
using Avalonia.Data.Converters;
using SharplexTimeCode.Helper;

namespace SharplexTimeCode.CustomControls.Converter;

public class DateOnlyConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is DateOnly dateOnly)
        {
            return ConverterHelper.GetFullMonthName(dateOnly.Month);
        }

        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}