using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Marketer.Desktop;

public class NavigationService
{
    private readonly IServiceProvider _serviceProvider;

    private readonly MainWindow _mainWindow;

    public NavigationService(IServiceProvider serviceProvider, MainWindow mainWindow)
    {
        _serviceProvider = serviceProvider;
        _mainWindow = mainWindow;
    }

    public void NavigateTo<T>() where T : Page
    {
        var page = _serviceProvider.GetRequiredService<T>();
        _mainWindow.NavigateTo(page);
    }
}