using System;

namespace SharplexTimeCode.CustomControls.Models;

public class DateOnlyModel(DateOnly value)
{
    public DateOnly Value { get; set; } = value;
}