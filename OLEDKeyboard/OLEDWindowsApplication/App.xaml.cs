using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace OLEDWindowsApplication;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private IServiceProvider _serviceProvider;

    private void ConfigureServices(ServiceCollection services)
    {
        services.AddSingleton<Configuration>();
        services.AddSingleton<LayoutManager>();
        services.AddSingleton<HardwareDataBridge>();
        services.AddSingleton<MainWindow>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        
        _serviceProvider = services.BuildServiceProvider();
        
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}