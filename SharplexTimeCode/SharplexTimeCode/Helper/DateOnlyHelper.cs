using System;

namespace SharplexTimeCode.Helper;

public static class DateOnlyHelper
{
    public static bool IsFirstOrLastDay(this DateOnly date)
    {
        return date.Day == 1 || DateTime.DaysInMonth(date.Year, date.Month) == date.Day;
    }
}