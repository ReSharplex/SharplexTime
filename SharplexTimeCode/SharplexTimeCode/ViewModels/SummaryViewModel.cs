using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Avalonia.Threading;
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

        var dispatcherTime = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(1)
        };

        dispatcherTime.Tick += (_, _) => UpdateTime();
        
        PlayCommand = ReactiveCommand.Create(() =>
        {
            var responseTuple = bookingsTemp.StartBookingPerButton(DateTime.Now, SelectedBookingType?.Id);
            if (responseTuple.responseResult.IsSuccess == ResponseStatus.Success)
            {
                SetButtonContent("Timer", "LightGreen", this);

                if (responseTuple.bookingTypeId is not null)
                {
                    _timeSpan = _cacheTimeSpan;
                    SelectedBookingType = BookingTypes.FirstOrDefault(a => a.Id == responseTuple.bookingTypeId);
                };
                
                dispatcherTime.Start();
                return;
            }

            mainWindowViewModel.SetNotifyControl(responseTuple.responseResult);
        });
        PauseCommand = ReactiveCommand.Create(() =>
        {
            var response = bookingsTemp.PauseBookingPerButton(DateTime.Now);
            if (response.IsSuccess == ResponseStatus.Success)
            {
                SetButtonContent("Pause", "Orange", this);
                _cacheTimeSpan = _timeSpan;
                _timeSpan = TimeSpan.FromSeconds(0);
                SelectedBookingType = BookingTypes.First(a => a.Id == 162);
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
                dispatcherTime.Stop();
                _timeSpan = TimeSpan.FromSeconds(0);
                TimeText = "00:00";
                SelectedBookingType = null;
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

    private void UpdateTime()
    {
        _timeSpan = _timeSpan.Add(TimeSpan.FromSeconds(1));
        TimeText = _timeSpan.ToString(@"hh\:mm");
    }

    private TimeSpan _timeSpan = TimeSpan.FromSeconds(0);
    private TimeSpan _cacheTimeSpan = TimeSpan.FromSeconds(0);

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

    private string _timeText = "00:00";

    public string TimeText
    {
        get => _timeText;
        set => this.RaiseAndSetIfChanged(ref _timeText, value);
    }
    
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
    
    private BookingTypeUI? _selectedBookingType;
    public BookingTypeUI? SelectedBookingType
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