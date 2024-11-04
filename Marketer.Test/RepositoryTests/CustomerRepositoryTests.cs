using FluentAssertions;
using Marketer.Data.Models;
using Marketer.Data.Repositories;
using Marketer.Data.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Marketer.Test.RepositoryTests;

public class CustomerRepositoryTests : TestBase
{
    private readonly IServiceProvider _serviceProvider;

    public CustomerRepositoryTests()
    {
        _serviceCollection.AddSingleton<ICustomerRepository, CustomerRepository>();
        _serviceProvider = _serviceCollection.BuildServiceProvider();
    }

    [Fact]
    public async Task CreateCustomer_Success()
    {
        //Arrange
        var customer = new CustomerModel
        {
            Id = Guid.NewGuid(),
            Age = 20,
            FirstName = "A",
            LastName = "B",
        };

        var customerRepository = _serviceProvider.GetRequiredService<ICustomerRepository>();

        //Act
        await customerRepository.Add(customer, _cancellationToken);

        //Assert
        var createdCustomer = await customerRepository.Get(customer.Id, _cancellationToken);

        createdCustomer.Should().NotBeNull();
        createdCustomer.Id.Should().Be(customer.Id);
        createdCustomer.FirstName.Should().Be(customer.FirstName);
        createdCustomer.LastName.Should().Be(customer.LastName);
    }

    [Fact]
    public async Task UpdateCustomer_Success()
    {
        //Create new customer
        var customer = new CustomerModel
        {
            Id = Guid.NewGuid(),
            Age = 20,
            FirstName = "A",
            LastName = "B",
        };

        var customerRepository = _serviceProvider.GetRequiredService<ICustomerRepository>();

        await customerRepository.Add(customer, _cancellationToken);

        //Assert customer created
        var createdCustomer = await customerRepository.Get(customer.Id, _cancellationToken);

        createdCustomer.Should().NotBeNull();
        createdCustomer.Id.Should().Be(customer.Id);
        createdCustomer.FirstName.Should().Be(customer.FirstName);
        createdCustomer.LastName.Should().Be(customer.LastName);

        //Change age
        customer.Age = 25;

        //Update customer
        await customerRepository.Update(customer, _cancellationToken);

        createdCustomer = await customerRepository.Get(customer.Id, _cancellationToken);

        //Assert age updated
        createdCustomer.Age.Should().Be(25);
    }

    [Fact]
    public async Task DeleteCustomer_Success()
    {
        //Create new customer
        var customer = new CustomerModel
        {
            Id = Guid.NewGuid(),
            Age = 20,
            FirstName = "A",
            LastName = "B",
        };

        var customerRepository = _serviceProvider.GetRequiredService<ICustomerRepository>();

        await customerRepository.Add(customer, _cancellationToken);

        //Assert customer created
        var createdCustomer = await customerRepository.Get(customer.Id, _cancellationToken);

        createdCustomer.Should().NotBeNull();

        await customerRepository.Delete(customer, _cancellationToken);

        createdCustomer = await customerRepository.Get(customer.Id, _cancellationToken);

        createdCustomer.Should().BeNull();
    }
}