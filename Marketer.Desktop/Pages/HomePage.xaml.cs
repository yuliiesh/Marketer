using System.Windows;
using Bogus;
using Marketer.Common.Orders;
using Marketer.Common.Orders.Create;
using Marketer.Data;
using Marketer.Data.Models;
using Marketer.Data.Repositories.Interfaces;
using Marketer.Desktop.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Marketer.Desktop.Pages;

public partial class HomePage
{
    private readonly NavigationService _navigation;
    private readonly IServiceProvider _serviceProvider;

    public HomePage(NavigationService navigation)
    {
        _navigation = navigation;
        InitializeComponent();
        _serviceProvider = ((App) Application.Current).ServiceProvider;
    }

    private void AllCustomers_Click(object sender, RoutedEventArgs e)
    {
        var customersWindow = _serviceProvider.GetRequiredService<CustomersWindow>();
        customersWindow.Show();
    }

    private void AllOrders_Click(object sender, RoutedEventArgs e)
    {
        _navigation.NavigateTo<OrderDetailsPage>();
    }

    private void AddCustomer_Click(object sender, RoutedEventArgs e)
    {
        _navigation.NavigateTo<CreateCustomerPage>();
    }

    private void ShowCustomersDiscounts_Click(object sender, RoutedEventArgs e)
    {
        _navigation.NavigateTo<CustomerDiscountPage>();
    }

    private void AddOrder_Click(object sender, RoutedEventArgs e)
    {
        _navigation.NavigateTo<OrderCreationPage>();
    }

    private async void GenerateTestData_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var context = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            await GenerateFakeCustomers(_serviceProvider, 10);
            context.ChangeTracker.Clear();
            await GenerateFakeOrders(_serviceProvider, 10, 10);

            MessageBox.Show("Test data generated successfully!\nRegister with any credentials to have access to app", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            _navigation.NavigateTo<AuthorizationPage>();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.InnerException?.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        return;

        async Task GenerateFakeCustomers(IServiceProvider provider, int count)
        {
            var customerFaker = new Faker<CustomerModel>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .RuleFor(c => c.Age, f => f.Random.Int(18, 65));

            ICustomerRepository customerRepository = provider.GetRequiredService<ICustomerRepository>();

            var data = customerFaker.Generate(count);

            foreach (var customer in data)
            {
                await customerRepository.Add(customer, CancellationToken.None);
            }
        }

        async Task GenerateFakeOrders(IServiceProvider provider, int maxOrderCount, int maxProductCount)
        {
            var customerRepository = provider.GetRequiredService<ICustomerRepository>();
            var customers = await customerRepository.GetAll(CancellationToken.None);

            var productFaker = new Faker<ProductModel>()
                .RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.Name, f => f.Vehicle.Model())
                .RuleFor(p => p.Price, f => f.Random.Decimal(10, 50));

            var orderFaker = new Faker<CreateOrderRequest>()
                .RuleFor(o => o.CreationDate, f => f.Date.Past())
                .RuleFor(o => o.CustomerId, f => f.PickRandom(customers.Select(c => c.Id)))
                .RuleFor(o => o.Products, f => productFaker.GenerateBetween(1, maxProductCount));

            var orders = orderFaker.Generate(maxOrderCount);
            var context = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            var orderHandler = provider.GetRequiredService<IOrderHandler>();
            foreach (var order in orders)
            {
                await orderHandler.CreateOrder(order, CancellationToken.None);
                context.ChangeTracker.Clear();
            }
        }
    }
}