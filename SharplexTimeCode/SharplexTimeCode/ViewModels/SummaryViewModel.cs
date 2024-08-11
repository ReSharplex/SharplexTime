using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using SharplexTimeCode.Core.Models;
using SharplexTimeCode.Core.Services;
using SharplexTimeCode.Core.Temp;
using SharplexTimeCode.Models;
using static SharplexTimeCode.Helper.ButtonHelper;

namespace SharplexTimeCode.ViewModels;

public class SummaryViewModel : PageViewModel
{
    public SummaryViewModel(MainWindowViewModel mainWindowViewModel, IServiceProvider provider)
    {
        mainWindowViewModel.Height = 600;
        mainWindowViewModel.Width = 500;
        
        var bookingsTemp = provider.GetRequiredService<IBookingsTemp>();

        SetButtonContent("Select", "#F1C40F", this);

        PlayCommand = ReactiveCommand.Create(() =>
        {
            var response = bookingsTemp.StartBookingPerButton(DateTime.Now, SelectedBookingType?.Id);
            if (response.IsSuccess == ResponseStatus.Success)
            {
                SetButtonContent("Timer", "LightGreen", this);
                return;
            }

            mainWindowViewModel.SetNotifyControl(response);
        });
        PauseCommand = ReactiveCommand.Create(() =>
        {
            var response = bookingsTemp.PauseBookingPerButton(DateTime.Now);
            if (response.IsSuccess == ResponseStatus.Success)
            {
                SetButtonContent("Pause", "Orange", this);
                return;
            }
            
            mainWindowViewModel.SetNotifyControl(response);
        });
        StopCommand = ReactiveCommand.Create(() =>
        {
            var response = bookingsTemp.EndBookingPerButton();
            if (response.IsSuccess == ResponseStatus.Success)
            {
                SetButtonContent("Select", "#F1C40F", this);
                return;
            }
            
            mainWindowViewModel.SetNotifyControl(response);
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

        SetDetailedViewCommand = ReactiveCommand.Create(() =>
        {
            if (mainWindowViewModel.SelectedPage is null)
            {
                mainWindowViewModel.Height = 600;
                mainWindowViewModel.SelectedPage = mainWindowViewModel.Pages[2];
            }
            else
            {
                mainWindowViewModel.Height = 50;
                mainWindowViewModel.SelectedPage = null;
            }
            
            RaiseCloseMenuButtonFlyout();
        });
        
        RefreshTypesCommand.Execute(null);
    }

    public ICommand PlayCommand { get; }
    public ICommand PauseCommand { get; }
    public ICommand StopCommand { get; }
    public ICommand RefreshTypesCommand { get; }
    public ICommand SetDetailedViewCommand { get; }
    
    public delegate void ComboBoxPressedOrReleased(bool isPressed = true);
    public event ComboBoxPressedOrReleased ComboBoxPressedOrReleasedEvent;

    public delegate void CloseActionButtonFlyout();
    public event CloseActionButtonFlyout CloseActionButtonFlyoutEvent;
    
    public delegate void CloseMenuButtonFlyout();
    public event CloseMenuButtonFlyout CloseMenuButtonFlyoutEvent;
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

    public void RaiseComboBoxPressedOrReleased(bool isPressed = true)
    {
        ComboBoxPressedOrReleasedEvent?.Invoke(isPressed);
    }
    
    public void RaiseCloseActionButtonFlyout()
    {
        CloseActionButtonFlyoutEvent?.Invoke();
    }
    
    public void RaiseCloseMenuButtonFlyout()
    {
        CloseMenuButtonFlyoutEvent?.Invoke();
    }
}