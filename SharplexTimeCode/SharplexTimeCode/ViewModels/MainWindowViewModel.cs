using System.Collections.ObjectModel;
using ReactiveUI;

namespace SharplexTimeCode.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        SelectedPage = new SummaryViewModel(this);
    }
    
    public ObservableCollection<PageViewModel> Pages { get; } = [];

    private PageViewModel _selectedPage;
    public PageViewModel SelectedPage
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