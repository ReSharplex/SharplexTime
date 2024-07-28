using System;
using SharplexTimeCode.Helper;

namespace SharplexTimeCode.CustomControls.Models;

public class DateOnlyModel(DateOnly value, bool isFirstOrLastDay = false)
{
    public DateOnly Value { get; set; } = value;
    
    public bool IsFirstOrLastDay { get; set; } = isFirstOrLastDay;
    
    public string DayOfWeek => IsFirstOrLastDay
        ? ConverterHelper.GetMonthName(Value.Month)
        : ConverterHelper.ModifyDay(Value.DayOfWeek.ToString());

    public string Color => IsFirstOrLastDay
        ? "Orange"
        : "Gray";
}