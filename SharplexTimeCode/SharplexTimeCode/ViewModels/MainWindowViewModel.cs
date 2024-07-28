using System;
using System.Collections.ObjectModel;
using ReactiveUI;

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
        
        // ...
        Pages.Add(new SummaryViewModel(this, provider));
        Pages.Add(new DetailedViewModel(this, provider));
        TopControl = Pages[0];
    }
    
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