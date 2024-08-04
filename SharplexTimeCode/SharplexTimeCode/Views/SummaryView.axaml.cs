using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Interactivity;
using SharplexTimeCode.ViewModels;

namespace SharplexTimeCode.Views;

public partial class SummaryView : UserControl
{
    public SummaryView()
    {
        InitializeComponent();
    }
    
    private void ViewModelOnCloseActionButtonFlyoutEvent()
    {
        ActionButton.Flyout?.Hide();
    }
    private void ViewModelOnCloseMenuButtonFlyoutEvent()
    {
        MenuButton.Flyout?.Hide();
    }

    private void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        if (DataContext is SummaryViewModel model)
        {
            model.CloseActionButtonFlyoutEvent += ViewModelOnCloseActionButtonFlyoutEvent;
            model.CloseMenuButtonFlyoutEvent += ViewModelOnCloseMenuButtonFlyoutEvent;
        }
    }

    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (DataContext is SummaryViewModel viewModel) viewModel.RaiseComboBoxPressedOrReleased();
    }

    private void NonClickable_OnPointerExited(object? sender, PointerEventArgs e)
    {
        if (DataContext is SummaryViewModel viewModel) viewModel.RaiseComboBoxPressedOrReleased(false);
    }
}