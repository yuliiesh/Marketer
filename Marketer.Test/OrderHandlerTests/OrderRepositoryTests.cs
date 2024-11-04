using FluentAssertions;
using Marketer.Common.Orders;
using Marketer.Common.Orders.Create;
using Marketer.Data.Models;
using Marketer.Data.Repositories;
using Marketer.Data.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Marketer.Test.OrderHandlerTests;

public class OrderRepositoryTests: TestBase
{
    public OrderRepositoryTests()
    {
        _serviceCollection.AddSingleton<IOrderHandler, OrderHandler>();
        _serviceCollection.AddSingleton<ICustomerRepository, CustomerRepository>();
        _serviceCollection.AddSingleton<IOrderRepository, Data.Repositories.OrderRepository>();
        _serviceCollection.AddSingleton<IProductRepository, ProductRepository>();

        _serviceProvider = _serviceCollection.BuildServiceProvider();
    }

    [Fact]
    public async Task CreateOrder_Success()
    {
        var customerRepository = _serviceProvider.GetService<ICustomerRepository>();
        await CreateCustomers(customerRepository);
        var customers = await customerRepository.GetAll(_cancellationToken);

        var request = new CreateOrderRequest
        {
            CustomerId = customers.First().Id,
            CreationDate = DateTime.Now,
            Products =
            [
                new()
                {
                    Id = Guid.NewGuid(),
                    Price = 10,
                    Name = "Test1"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Price = 100,
                    Name = "Test2"
                }
            ],
        };

        var handler = _serviceProvider.GetRequiredService<IOrderHandler>();
        var response = await handler.CreateOrder(request, _cancellationToken);
        response.Should().NotBeNull();
        response.TotalPrice.Should().Be(request.Products.Sum(product => product.Price));

        var customer = await customerRepository.GetWithOrders(request.CustomerId, _cancellationToken);

        var order =  customer.Orders.FirstOrDefault();

        order.Should().NotBeNull();
        order.Id.Should().Be(response.OrderId);

        var products = order.Products;

        products.Should().NotBeNullOrEmpty();
        products.Should().HaveCount(2);
    }

    private async Task CreateCustomers(ICustomerRepository customerRepository)
    {
        for (int i = 0; i < 2; i++)
        {
            var customer = new CustomerModel
            {
                Id = Guid.NewGuid(),
                Age = 20 + i,
                FirstName = "Test" + i,
                LastName = "Test" + i
            };
            await customerRepository.Add(customer, _cancellationToken);
        }

        ClearTracker();
    }
}