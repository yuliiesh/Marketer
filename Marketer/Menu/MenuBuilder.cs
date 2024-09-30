using Bogus;
using Marketer.Authorization;
using Marketer.Authorization.Login;
using Marketer.Authorization.Registration;
using Marketer.Customers;
using Marketer.Customers.Create;
using Marketer.Customers.Read;
using Marketer.Customers.Select;
using Marketer.Data;
using Marketer.Data.Models;
using Marketer.Discounts;
using Marketer.Discounts.Create;
using Marketer.Discounts.Read;
using Marketer.Orders;
using Marketer.Orders.Create;
using Marketer.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Marketer.Menu;

public class MenuBuilder
{
    private readonly IServiceProvider _serviceProvider;

    private IMenu mainMenu;

    public MenuBuilder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

    }

    public IReadOnlyList<MenuItem> Build()
    {
        IReadOnlyList<MenuItem> items =
        [
            new()
            {
                Title = "Customer",
                SubItems = BuildCustomerSection()
            },
            new()
            {
                Title = "Order",
                SubItems = BuildOrderSection()
            },
            new()
            {
                Title = "Discount",
                SubItems = BuildDiscountSection()
            },
            new()
            {
                Title = "Logout",
                Action = () => { mainMenu.LogoutPressed += () => true; return Task.CompletedTask; }
            },
            new()
            {
                Title = "Generate test data",
                Action = GenerateTestData
            },
            new()
            {
                Title = "Exit",
                Action = async () => Environment.Exit(0)
            }
        ];

        return items;
    }

    public IReadOnlyList<MenuItem> BuildRegistrationItems()
    {
        IReadOnlyList<MenuItem> items =
        [
            new()
            {
                Title = "Login",
                Action = Login,
            },

            new()
            {
                Title = "Register",
                Action = Register,
            },
        ];

        mainMenu = _serviceProvider.GetRequiredKeyedService<IMenu>("MainMenu");
        return items;
    }

    private IReadOnlyList<MenuItem> BuildDiscountSection()
    {
        return
        [
            new()
            {
                Title = "Add discount",
                Action = AddDiscount
            },
            new()
            {
                Title = "Customers discounts",
                Action = ShowCustomersDiscounts
            }
        ];
    }

    private IReadOnlyList<MenuItem> BuildCustomerSection()
    {
        return
        [
            new()
            {
                Title = "Add Customer",
                Action = AddCustomer
            },
            new()
            {
                Title = "All Customers",
                Action = AllCustomers
            },
            new()
            {
                Title = "Delete Customer",
                Action = DeleteCustomer
            }
        ];
    }

    private IReadOnlyList<MenuItem> BuildOrderSection()
    {
        return
        [
            new MenuItem
            {
                Title = "Add Order",
                Action = AddOrder
            }
        ];
    }

    private async Task ShowCustomersDiscounts()
    {
        var action = _serviceProvider.GetRequiredService<ReadCustomerDiscountsAction>();
        await action.Invoke(CancellationToken.None);
    }

    private async Task AllCustomers()
    {
        var action = _serviceProvider.GetRequiredService<ReadCustomersAction>();

        await action.Invoke(CancellationToken.None);
    }

    private async Task AddOrder()
    {
        var createOrderAction = _serviceProvider.GetRequiredService<CreateOrderAction>();
        var handler = _serviceProvider.GetRequiredService<IOrderHandler>();

        var request = createOrderAction.Invoke();

        await handler.CreateOrder(request, CancellationToken.None);

        Console.WriteLine("Order created");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    private async Task AddCustomer()
    {
        var addCustomerAction = _serviceProvider.GetRequiredService<CreateCustomerAction>();
        var handler = _serviceProvider.GetRequiredService<ICustomerHandler>();

        var request = addCustomerAction.Invoke();

        var response = await handler.Create(request, CancellationToken.None);

        Console.WriteLine("Customer Added");
        var customer = response.Customer;
        Console.WriteLine($"Name: {customer.FirstName} Last Name: {customer.LastName} Age: {customer.Age}");
        Console.WriteLine("Press any key to continue");
        Console.Read();
    }

    private async Task AddDiscount()
    {
        var selectCustomerAction = _serviceProvider.GetRequiredService<SelectCustomerAction>();
        var createDiscountAction = _serviceProvider.GetRequiredService<CreateDiscountAction>();
        var discountHandler = _serviceProvider.GetRequiredService<IDiscountHandler>();

        var customer = await selectCustomerAction.Invoke(CancellationToken.None);

        var createDiscountRequest = createDiscountAction.Invoke(customer);
        CreateDiscountResponse createDiscountResponse;
        try
        {
            createDiscountResponse = await discountHandler.CreateDiscount(createDiscountRequest, CancellationToken.None);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"Discount ({createDiscountResponse.Discount}%) for customer {customer.FirstName} {customer.LastName} created");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    private async Task DeleteCustomer()
    {
        var action = _serviceProvider.GetRequiredService<SelectCustomerAction>();
        var handler = _serviceProvider.GetRequiredService<ICustomerHandler>();

        var customer = await action.Invoke(CancellationToken.None);

        Console.WriteLine("Are you sure you want to delete the customer? y/n");

        var input = Console.ReadKey(true);

        while (input.Key != ConsoleKey.Y && input.Key != ConsoleKey.N)
        {
            input = Console.ReadKey(true);
        }

        if (input.Key == ConsoleKey.Y)
        {
             await handler.Delete(customer, CancellationToken.None);

            Console.WriteLine($"Customer {customer.FirstName} {customer.LastName} Deleted");
        }

        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    private async Task GenerateTestData()
    {
        var context = _serviceProvider.GetRequiredService<ApplicationDbContext>();
        await GenerateFakeCustomers(_serviceProvider, 10);
        context.ChangeTracker.Clear();
        await GenerateFakeOrders(_serviceProvider, 10, 10);

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
    }

    #region Authorization

    private async Task Login()
    {
        var loginAction = _serviceProvider.GetRequiredService<LoginAction>();
        var loginHandler = _serviceProvider.GetRequiredService<ILoginHandler>();

        var loginRequest = loginAction.Invoke();
        var loginResponse = await loginHandler.Login(loginRequest, CancellationToken.None);

        if (loginResponse.Success)
        {
            await mainMenu.Display();
        }
        else
        {
            Console.WriteLine(loginResponse.ErrorMessage);
            Console.WriteLine("Press any key to try again");
        }
    }

    private async Task Register()
    {
        var registrationAction = _serviceProvider.GetRequiredService<RegistrationAction>();
        var loginHandler = _serviceProvider.GetRequiredService<ILoginHandler>();

        var registrationRequest = registrationAction.Invoke();
        var registrationResponse = await loginHandler.Register(registrationRequest, CancellationToken.None);

        if (registrationResponse.Success)
        {
            await mainMenu.Display();
        }
        else
        {
            Console.WriteLine(registrationResponse.ErrorMessage);
        }
    }

    #endregion
}