using System.Collections.ObjectModel;
using ReactiveUI;
using SharplexTimeCode.Models;

namespace SharplexTimeCode.ViewModels;

public class SummaryViewModel : PageViewModel
{
    public SummaryViewModel(MainWindowViewModel mainWindowViewModel)
    {
        mainWindowViewModel.Height = 50;
        mainWindowViewModel.Width = 500;
        
        BookingTypes.Add(new BookingTypeUI() {Id = 0, Title = "Normalbuchung"});
        BookingTypes.Add(new BookingTypeUI() {Id = 1, Title = "Arzttermin"});
        BookingTypes.Add(new BookingTypeUI() {Id = 2, Title = "Schule"});
    }
    
    public ObservableCollection<BookingTypeUI> BookingTypes { get; } = [];

    private BookingTypeUI _selectedBookingType;
    public BookingTypeUI SelectedBookingType
    {
        get => _selectedBookingType;
        set => this.RaiseAndSetIfChanged(ref _selectedBookingType, value);
    }
}