using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using SharplexTimeCode.Core.Services;
using SharplexTimeCode.Models;
using static SharplexTimeCode.Helper.ButtonHelper;

namespace SharplexTimeCode.ViewModels;

public class SummaryViewModel : PageViewModel
{
    public SummaryViewModel(MainWindowViewModel mainWindowViewModel, IServiceProvider provider)
    {
        mainWindowViewModel.Height = 50;
        mainWindowViewModel.Width = 500;

        SetButtonContent("Select", "#F1C40F", this);
        
        PlayCommand = ReactiveCommand.Create(() => SetButtonContent("Timer", "LightGreen", this));
        PauseCommand = ReactiveCommand.Create(() => SetButtonContent("Pause", "Orange", this));
        StopCommand = ReactiveCommand.Create(() => SetButtonContent("Select", "#F1C40F", this));
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

        SetDetailedViewCommand = ReactiveCommand.Create(() =>
        {
            if (mainWindowViewModel.SelectedPage is null)
            {
                mainWindowViewModel.Height = 600;
                mainWindowViewModel.SelectedPage = mainWindowViewModel.Pages[1];
                return;
            }

            mainWindowViewModel.Height = 50;
            mainWindowViewModel.SelectedPage = null;
        });
        
        RefreshTypesCommand.Execute(null);
    }

    public ICommand PlayCommand { get; }
    public ICommand PauseCommand { get; }
    public ICommand StopCommand { get; }
    public ICommand RefreshTypesCommand { get; }
    public ICommand SetDetailedViewCommand { get; }
    public ObservableCollection<BookingTypeUI> BookingTypes { get; } = [];
    
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
    
    private BookingTypeUI _selectedBookingType;
    public BookingTypeUI SelectedBookingType
    {
        get => _selectedBookingType;
        set => this.RaiseAndSetIfChanged(ref _selectedBookingType, value);
    }
}