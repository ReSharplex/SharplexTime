using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using SharplexTimeCode.Core.Services;
using SharplexTimeCode.Models;

namespace SharplexTimeCode.ViewModels;

public class SummaryViewModel : PageViewModel
{
    public SummaryViewModel(MainWindowViewModel mainWindowViewModel, IServiceProvider provider)
    {
        mainWindowViewModel.Height = 50;
        mainWindowViewModel.Width = 500;
        
        ActionButtonContent = "Select";
        ActionButtonForeground = "#F1C40F";
        
        PlayCommand = ReactiveCommand.Create(() =>
        {
            ActionButtonContent = "Timer";
            ActionButtonForeground = "LightGreen";
        });
        PauseCommand = ReactiveCommand.Create(() =>
        {
            ActionButtonContent = "Pause";
            ActionButtonForeground = "Orange";
        });
        StopCommand = ReactiveCommand.Create(() =>
        {
            ActionButtonContent = "Select";
            ActionButtonForeground = "#F1C40F";
        });
        RefreshTypesCommand = ReactiveCommand.Create(() =>
        {
            var bookingTypes = provider.GetRequiredService<IBookingTypeService>().GetBookingTypes();
            BookingTypes.Clear();
            foreach (var bookingType in bookingTypes)
            {
                BookingTypes.Add(new BookingTypeUI()
                {
                    Id = bookingType.Id,
                    Title = bookingType.Title
                });
            }
        });
        
        RefreshTypesCommand.Execute(null);
    }

    public ICommand PlayCommand { get; }
    public ICommand PauseCommand { get; }
    public ICommand StopCommand { get; }
    public ICommand RefreshTypesCommand { get; }
    
    private string _actionButtonContent;
    public string ActionButtonContent
    {
        get => _actionButtonContent;
        set => this.RaiseAndSetIfChanged(ref _actionButtonContent, value);
    }
    
    private string _actionButtonForeground;
    public string ActionButtonForeground
    {
        get => _actionButtonForeground;
        set => this.RaiseAndSetIfChanged(ref _actionButtonForeground, value);
    }
    
    public ObservableCollection<BookingTypeUI> BookingTypes { get; } = [];
    
    private BookingTypeUI _selectedBookingType;
    public BookingTypeUI SelectedBookingType
    {
        get => _selectedBookingType;
        set => this.RaiseAndSetIfChanged(ref _selectedBookingType, value);
    }
}