using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SharplexTimeCode.Configuration;
using SharplexTimeCode.Core.Models;
using SharplexTimeCode.Core.Repositories;
using SharplexTimeCode.Core.Services;
using SharplexTimeCode.ViewModels;
using SharplexTimeCode.Views;

namespace SharplexTimeCode;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var collection = new ServiceCollection();
        collection.ConfigureUI();

        var provider = collection.BuildServiceProvider();
        
        // provider.GetRequiredService<IBookingTypeService>().CreateSampleBookingTypes();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(provider)
            };
        }
        
        base.OnFrameworkInitializationCompleted();
    }
}