using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FluentAssertions;
using Marketer.Data.Models;
using Marketer.Repositories;
using Marketer.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Marketer.Test.RepositoryTests;

public class OrderRepositoryTests : TestBase
{
    public OrderRepositoryTests()
    {
        _serviceCollection.AddSingleton<IOrderRepository, OrderRepository>();
        _serviceProvider = _serviceCollection.BuildServiceProvider();
    }

    [Fact]
    public async Task CreateOrder_Success()
    {
        //Arrange
        var order = new OrderModel
        {
            Id = Guid.NewGuid(),
            CreationDate = new DateTime(2024, 9, 27),
            Products = new Collection<ProductModel>
            {
                new() { Id = Guid.NewGuid(), Name = "Product A", Price = 10m }
            },
            TotalPrice = 100m
        };

        var orderRepository = _serviceProvider.GetRequiredService<IOrderRepository>();

        //Act
        await orderRepository.Add(order, _cancellationToken);

        //Assert
        var createdOrder = await orderRepository.Get(order.Id, _cancellationToken);

        createdOrder.Should().NotBeNull();
        createdOrder.Id.Should().Be(order.Id);
        createdOrder.CreationDate.Should().Be(order.CreationDate);
        createdOrder.Products.Should().BeEquivalentTo(order.Products);
    }

    [Fact]
    public async Task UpdateOrder_Success()
    {
        //Create new customer
        var order = new OrderModel
        {
            Id = Guid.NewGuid(),
            CreationDate = new DateTime(2024, 9, 30),
            Products = new Collection<ProductModel>
            {
                new() { Id = Guid.NewGuid(), Name = "Product B", Price = 20m }
            },
            TotalPrice = 100,
        };

        var orderRepository = _serviceProvider.GetRequiredService<IOrderRepository>();

        await orderRepository.Add(order, _cancellationToken);

        //Assert customer created
        var createdOrder = await orderRepository.Get(order.Id, _cancellationToken);

        createdOrder.Should().NotBeNull();
        createdOrder.Id.Should().Be(order.Id);
        createdOrder.CreationDate.Should().Be(order.CreationDate);
        createdOrder.Products.Should().BeEquivalentTo(order.Products);

        //Change CreationDate
        order.CreationDate = new DateTime(2024, 10, 1);

        //Update customer
        await orderRepository.Update(order, _cancellationToken);

        createdOrder = await orderRepository.Get(order.Id, _cancellationToken);

        //Assert age updated
        createdOrder.CreationDate.Should().Be(new DateTime(2024, 10, 1));
    }

    [Fact]
    public async Task DeleteOrder_Success()
    {
        //Create new customer
        var order = new OrderModel
        {
            Id = Guid.NewGuid(),
            CreationDate = new DateTime(2024, 9, 27),
            Products = new Collection<ProductModel>(),
            TotalPrice = 100m
        };

        var orderRepository = _serviceProvider.GetRequiredService<IOrderRepository>();

        await orderRepository.Add(order, _cancellationToken);

        //Assert customer created
        var createdOrder = await orderRepository.Get(order.Id, _cancellationToken);

        createdOrder.Should().NotBeNull();

        await orderRepository.Delete(order, _cancellationToken);

        createdOrder = await orderRepository.Get(order.Id, _cancellationToken);

        createdOrder.Should().BeNull();
    }
}