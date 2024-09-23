using FluentAssertions;
using Marketer.Data.Models;
using Marketer.Repositories;
using Marketer.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace Marketer.Test;

public class CustomerRepositoryTests : TestBase
{
    private readonly IServiceProvider _serviceProvider;

    public CustomerRepositoryTests()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton(_ => _context);
        serviceCollection.AddSingleton<ICustomerRepository, CustomerRepository>();
        _serviceProvider = serviceCollection.BuildServiceProvider();
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
        var cancellationToken = CancellationToken.None;
        var customerRepository = _serviceProvider.GetRequiredService<ICustomerRepository>();

        //Act
        await customerRepository.Add(customer, cancellationToken);

        //Assert
        var createdCustomer = await customerRepository.Get(customer.Id, cancellationToken);

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
        var cancellationToken = CancellationToken.None;
        var customerRepository = _serviceProvider.GetRequiredService<ICustomerRepository>();

        await customerRepository.Add(customer, cancellationToken);

        //Assert customer created
        var createdCustomer = await customerRepository.Get(customer.Id, cancellationToken);

        createdCustomer.Should().NotBeNull();
        createdCustomer.Id.Should().Be(customer.Id);
        createdCustomer.FirstName.Should().Be(customer.FirstName);
        createdCustomer.LastName.Should().Be(customer.LastName);

        //Change age
        createdCustomer.Age = 25;

        //Update customer
        await customerRepository.Update(customer, cancellationToken);

        createdCustomer = await customerRepository.Get(customer.Id, cancellationToken);

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
        var cancellationToken = CancellationToken.None;
        var customerRepository = _serviceProvider.GetRequiredService<ICustomerRepository>();

        await customerRepository.Add(customer, cancellationToken);

        //Assert customer created
        var createdCustomer = await customerRepository.Get(customer.Id, cancellationToken);

        createdCustomer.Should().NotBeNull();

        await customerRepository.Delete(customer, cancellationToken);

        createdCustomer = await customerRepository.Get(customer.Id, cancellationToken);

        createdCustomer.Should().BeNull();
    }
}