using Bogus;
using Marketer;
using Marketer.Customers;
using Marketer.Data;
using Marketer.Data.Models;
using Marketer.Menu;
using Marketer.Orders;
using Marketer.Orders.Create;
using Marketer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

serviceCollection.AddDbContextFactory<ApplicationDbContext>(options
    => options.UseSqlServer(@"Server=localhost\sqlexpress;Database=marketer;Trusted_Connection=True;Integrated Security=SSPI;TrustServerCertificate=True;"));

serviceCollection.AddRepositories()
    .AddActions()
    .AddHandlers();
serviceCollection.AddSingleton<MenuBuilder>();
serviceCollection.AddKeyedSingleton<IMenu, Menu>("MainMenu",(provider, _) =>
{
    var builder = provider.GetRequiredService<MenuBuilder>();
    var items = builder.Build();
    return new Menu(items);
});

serviceCollection.AddKeyedSingleton<IMenu, Menu>("AuthorizationMenu", (provider, _) =>
{
    var builder = provider.GetRequiredService<MenuBuilder>();
    var items = builder.BuildRegistrationItems();
    return new Menu(items);
});

var provider = serviceCollection.BuildServiceProvider();

var menu = provider.GetRequiredKeyedService<IMenu>("AuthorizationMenu");

try
{
    await menu.Display();
}
catch (Exception e)
{
    Console.WriteLine(e);
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}

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

    var orderHandler = provider.GetRequiredService<IOrderHandler>();
    var context = provider.GetRequiredService<ApplicationDbContext>();
    foreach (var order in orders)
    {
        await orderHandler.CreateOrder(order, CancellationToken.None);
        context.ChangeTracker.Clear();
    }
}