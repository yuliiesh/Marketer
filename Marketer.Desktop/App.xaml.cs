using System.Windows;
using Marketer.Common.Authorization;
using Marketer.Common.Customers;
using Marketer.Common.Discounts;
using Marketer.Common.Orders;
using Marketer.Data;
using Marketer.Data.Repositories;
using Marketer.Data.Repositories.Interfaces;
using Marketer.Desktop.Pages;
using Marketer.Desktop.ViewModels;
using Marketer.Desktop.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Marketer.Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    public IServiceProvider ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        ServiceProvider = serviceCollection.BuildServiceProvider();

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        var authorizationPage = ServiceProvider.GetRequiredService<AuthorizationPage>();

        var homePage = ServiceProvider.GetRequiredService<HomePage>();
        mainWindow.NavigateTo(homePage);
        mainWindow.Show();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<ILoginHandler, LoginHandler>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IOrderHandler, OrderHandler>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddScoped<ILoginHandler, LoginHandler>();
        services.AddScoped<ICustomerHandler, CustomerHandler>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IDiscountHandler, DiscountHandler>();

        services.AddSingleton<NavigationService>();
        ConfigureWindows(services);
        ConfigurePages(services);
        ConfigureViewModels(services);

        services.AddDbContextFactory<ApplicationDbContext>(options =>
            options.UseSqlServer(@"Server=localhost\sqlexpress;Database=marketer;Trusted_Connection=True;Integrated Security=SSPI;TrustServerCertificate=True;"));
    }

    private static void ConfigureViewModels(IServiceCollection services)
    {
        services.AddSingleton<AuthorizationViewModel>();
        services.AddSingleton<CustomersViewModel>();
        services.AddSingleton<CreateCustomerViewModel>();
        services.AddSingleton<CustomerDiscountsViewModel>();
        services.AddSingleton<OrderCreationViewModel>();
        services.AddSingleton<OrderDetailsViewModel>();
    }

    private static void ConfigurePages(IServiceCollection services)
    {
        services.AddSingleton<AuthorizationPage>();
        services.AddSingleton<HomePage>();
        services.AddTransient<CreateCustomerPage>();
        services.AddTransient<CustomerDiscountPage>();
        services.AddTransient<OrderCreationPage>();
        services.AddTransient<OrderDetailsPage>();
    }

    private static void ConfigureWindows(IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services.AddTransient<CustomersWindow>();
    }
}