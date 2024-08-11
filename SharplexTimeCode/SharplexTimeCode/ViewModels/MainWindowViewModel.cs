using System;
using System.Collections.ObjectModel;
using Avalonia;
using ReactiveUI;
using SharplexTimeCode.Core.Models;

namespace SharplexTimeCode.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    // transfer later to detailedview
    private DateOnly _selectedDate;
    public DateOnly SelectedDate
    {
        get => _selectedDate;
        set => this.RaiseAndSetIfChanged(ref _selectedDate, value);
    }  
    
    public MainWindowViewModel(IServiceProvider provider)
    {
        // transfer later to detailedview

        SelectedDate = DateOnly.FromDateTime(DateTime.Now);
        
        //
        var summaryViewModel = new SummaryViewModel(this, provider);
        var errorViewModel = new NotifyViewModel(this, provider);
        var detailedViewModel = new DetailedViewModel(this, provider);
        
        //
        summaryViewModel.ComboBoxPressedOrReleasedEvent += SummaryViewModelOnComboBoxPressedOrReleasedEvent;
        
        // ...
        Pages.Add(summaryViewModel);
        Pages.Add(errorViewModel);
        Pages.Add(detailedViewModel);
        TopControl = Pages[0];
    }

    public void SetNotifyControl(ResponseResult result)
    {
        if (Pages[1] is not NotifyViewModel notifyViewModel) return;
        
        notifyViewModel.Message = result.Error;
        TopControl = notifyViewModel;
    }
    
    public void SetSummaryControl()
    {
        TopControl = Pages[0];
    }
    
    private void SummaryViewModelOnComboBoxPressedOrReleasedEvent(bool isPressed)
    {
        IsComboBoxPressed = isPressed;
    }
    
    public bool IsComboBoxPressed { get; set; }

    public ObservableCollection<PageViewModel> Pages { get; } = [];

    private PageViewModel _topControl;
    public PageViewModel TopControl
    {
        get => _topControl;
        set => this.RaiseAndSetIfChanged(ref _topControl, value);
    }
    
    private PageViewModel? _selectedPage;
    public PageViewModel? SelectedPage
    {
        get => _selectedPage;
        set => this.RaiseAndSetIfChanged(ref _selectedPage, value);
    }

    private int _height;
    public int Height
    {
        get => _height;
        set => this.RaiseAndSetIfChanged(ref _height, value);
    }
    
    private int _width;
    public int Width
    {
        get => _width;
        set => this.RaiseAndSetIfChanged(ref _width, value);
    }
} 