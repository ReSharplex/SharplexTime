using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using SharplexTimeCode.CustomControls.Models;
using SharplexTimeCode.Helper;

namespace SharplexTimeCode.CustomControls.Controls;

public class TimeCalendarControl : TemplatedControl
{
    private ItemsControl? _timeCalendarPresenter;

    public TimeCalendarControl()
    {
        UpdateDates();
    }

    public static readonly StyledProperty<int> MaxCountProperty = AvaloniaProperty.Register<TimeCalendarControl, int>
    (
        nameof(MaxCount),
        6,
        coerce: CoerceMaxCount
    );

    private static int CoerceMaxCount(AvaloniaObject avaloniaObject, int value)
    {
        return value > 15 ? 15 : value;
    }

    public static readonly DirectProperty<TimeCalendarControl, DateOnly> SelectedDateProperty =
        AvaloniaProperty.RegisterDirect<TimeCalendarControl, DateOnly>
        (
            nameof(SelectedDate),
            o => o.SelectedDate,
            (o, v) => o.SelectedDate = v
        );

    private DateOnly _selectedDate;
    public DateOnly SelectedDate
    {
        get => _selectedDate;
        set => SetAndRaise(SelectedDateProperty, ref _selectedDate, value);
    }

    public static readonly DirectProperty<TimeCalendarControl, string> SelectedMonthProperty =
        AvaloniaProperty.RegisterDirect<TimeCalendarControl, string>(
            nameof(SelectedMonth),
            o => o.SelectedMonth,
            (o, v) => o.SelectedMonth = v);
    
    private string _selectedMonth;
    public string SelectedMonth
    {
        get => _selectedMonth;
        set => SetAndRaise(SelectedMonthProperty, ref _selectedMonth, value);
    }
    
    public static readonly DirectProperty<TimeCalendarControl, string> SelectedYearProperty =
        AvaloniaProperty.RegisterDirect<TimeCalendarControl, string>(
            nameof(SelectedYear),
            o => o.SelectedYear,
            (o, v) => o.SelectedYear = v);
    
    private string _selectedYear;
    public string SelectedYear
    {
        get => _selectedYear;
        set => SetAndRaise(SelectedYearProperty, ref _selectedYear, value);
    }

    public static readonly DirectProperty<TimeCalendarControl, ObservableCollection<DateOnlyModel>> DatesOnliesProperty =
        AvaloniaProperty.RegisterDirect<TimeCalendarControl, ObservableCollection<DateOnlyModel>>(
            nameof(DatesOnlies),
            o => o.DatesOnlies,
            (o, v) => o.DatesOnlies = v);

    private ObservableCollection<DateOnlyModel> _datesOnlies;
    public ObservableCollection<DateOnlyModel> DatesOnlies
    {
        get => _datesOnlies;
        set => SetAndRaise(DatesOnliesProperty, ref _datesOnlies, value);
    }

    public int MaxCount
    {
        get => GetValue(MaxCountProperty);
        set => SetValue(MaxCountProperty, value);
    }

    private void UpdateDates()
    {
        var maxDate = SelectedDate.AddDays(MaxCount);
    
        DateOnlyModel Selector(int i)
        {
            var date = SelectedDate.AddDays(i);
            if (date.IsFirstOrLastDay())
            {
                return new(date, true);
            }
            
            return new(date);
        }

        var dateOnlies = Enumerable
            .Range(1, MaxCount)
            .Select(Selector)
            .TakeWhile(d => d.Value <= maxDate)
            .ToList();
        
        DatesOnlies = new(dateOnlies);
        
        IsBetweenMonthsAndYears();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == SelectedDateProperty)
        {
            UpdateDates();
        }
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        if (_timeCalendarPresenter is not null)
        {
            _timeCalendarPresenter.PointerReleased -= StarsPresenter_PointerReleased;
        }

        _timeCalendarPresenter = e.NameScope.Find("PART_StarsPresenter") as ItemsControl;

        if (_timeCalendarPresenter != null)
        {
            _timeCalendarPresenter.PointerReleased += StarsPresenter_PointerReleased;
        }
    }

    protected override void OnPointerWheelChanged(PointerWheelEventArgs e)
    {
        base.OnPointerWheelChanged(e);

        if (!e.Handled)
        {
            if (e.Delta.Y < 0)
            {
                DatesOnlies.Remove(DatesOnlies.FirstOrDefault());

                var date = DatesOnlies.Last().Value.AddDays(1);

                if (date.IsFirstOrLastDay())
                {
                    DatesOnlies.Add(new(date, true));
                }
                else
                {
                    DatesOnlies.Add(new(date));
                }
            }
            else
            {
                DatesOnlies.Remove(DatesOnlies.LastOrDefault());

                var date = DatesOnlies.FirstOrDefault()!.Value.AddDays(-1);

                DatesOnlies.Insert(0,
                    date.IsFirstOrLastDay() ? new DateOnlyModel(date, true) : new DateOnlyModel(date));
            }
            
            IsBetweenMonthsAndYears();

            e.Handled = true;
        }
    }

    public void IsBetweenMonthsAndYears()
    {
        IsBetweenMonths();
        IsBetweenYears();
    }
    
    private void IsBetweenMonths()
    {
        var firstDate = DatesOnlies.First();
        var lastDate = DatesOnlies.Last();
        
        SelectedMonth = firstDate.Value.Month == lastDate.Value.Month 
            ? ConverterHelper.GetFullMonthName(firstDate.Value.Month) 
            : $"{ConverterHelper.GetMonthName(firstDate.Value.Month)}/{ConverterHelper.GetMonthName(lastDate.Value.Month)}";
    }
    
    private void IsBetweenYears()
    {
        var firstDate = DatesOnlies.First();
        var lastDate = DatesOnlies.Last();
        
        SelectedYear = firstDate.Value.Year == lastDate.Value.Year 
            ? firstDate.Value.Year.ToString()
            : $"{firstDate.Value.Year}/{lastDate.Value.Year}";
    }
    
    private void StarsPresenter_PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e)
    {
        /*if (e.Source is Path star)
        {
            Value = star.DataContext as int? ?? 0;
        }*/
    }
}