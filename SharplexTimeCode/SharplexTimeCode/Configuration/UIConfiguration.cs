using Microsoft.Extensions.DependencyInjection;
using SharplexTimeCode.Core.Configuration;
using SharplexTimeCode.ViewModels;

namespace SharplexTimeCode.Configuration;

public static class UIConfiguration
{
    public static IServiceCollection ConfigureUI(this IServiceCollection serviceCollection)
    {
        var a = 0;
        
        serviceCollection.ConfigureCore();

        serviceCollection.AddSingleton<MainWindowViewModel>();
        serviceCollection.AddTransient<SummaryViewModel>();

        return serviceCollection;
    }
}