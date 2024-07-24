using SharplexTimeCode.ViewModels;

namespace SharplexTimeCode.Helper;

public static class ButtonHelper
{
    public static void SetButtonContent(string content, string foreground, SummaryViewModel viewModel)
    {
        viewModel.ActionButtonContent = content;
        viewModel.ActionButtonForeground = foreground;
        viewModel.RaiseCloseActionButtonFlyout();
    }
}