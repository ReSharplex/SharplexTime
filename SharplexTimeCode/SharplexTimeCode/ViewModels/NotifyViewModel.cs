using System;
using System.Windows.Input;
using ReactiveUI;

namespace SharplexTimeCode.ViewModels;

public class NotifyViewModel : PageViewModel
{
    public NotifyViewModel(MainWindowViewModel mainWindowViewModel, IServiceProvider provider)
    {
        BackCommand = ReactiveCommand.Create(() => mainWindowViewModel.SetSummaryControl());
    }

    private string _message;

    public string Message
    {
        get => _message;
        set => this.RaiseAndSetIfChanged(ref _message, value);
    }
    
    public ICommand BackCommand { get; }
}