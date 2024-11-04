using FluentAssertions;
using Marketer.Data.Models;
using Marketer.Data.Repositories;
using Marketer.Data.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Marketer.Test.RepositoryTests;

public class ProductRepositoryTests : TestBase
{
    public ProductRepositoryTests()
    {
        _serviceCollection.AddSingleton<IProductRepository, ProductRepository>();
        _serviceProvider = _serviceCollection.BuildServiceProvider();
    }

    [Fact]
    public async Task CreateProduct_Success()
    {
        //Arrange
        var product = new ProductModel
        {
            Id = Guid.NewGuid(),
            Name = "A",
            Price = 100,
        };

        var productRepository = _serviceProvider.GetRequiredService<IProductRepository>();

        //Act
        await productRepository.Add(product, _cancellationToken);

        //Assert
        var createdProduct = await productRepository.Get(product.Id, _cancellationToken);

        createdProduct.Should().NotBeNull();
        createdProduct.Id.Should().Be(product.Id);
        createdProduct.Name.Should().Be(product.Name);
        createdProduct.Price.Should().Be(product.Price);
    }

    [Fact]
    public async Task UpdateProduct_Success()
    {
        //Create new customer
        var product = new ProductModel
        {
            Id = Guid.NewGuid(),
            Name = "A",
            Price = 100,
        };

        var productRepository = _serviceProvider.GetRequiredService<IProductRepository>();

        await productRepository.Add(product, _cancellationToken);

        //Assert customer created
        var createdProduct = await productRepository.Get(product.Id, _cancellationToken);

        createdProduct.Should().NotBeNull();
        createdProduct.Id.Should().Be(product.Id);
        createdProduct.Name.Should().Be(product.Name);
        createdProduct.Price.Should().Be(product.Price);

        //Change age
        product.Price = 25;

        //Update customer
        await productRepository.Update(product, _cancellationToken);

        createdProduct = await productRepository.Get(product.Id, _cancellationToken);

        //Assert age updated
        createdProduct.Price.Should().Be(25);
    }

    [Fact]
    public async Task DeleteProduct_Success()
    {
        //Create new customer
        var product = new ProductModel
        {
            Id = Guid.NewGuid(),
            Name = "A",
            Price = 100,
        };

        var productRepository = _serviceProvider.GetRequiredService<IProductRepository>();

        await productRepository.Add(product, _cancellationToken);

        //Assert customer created
        var createdProduct = await productRepository.Get(product.Id, _cancellationToken);

        createdProduct.Should().NotBeNull();

        await productRepository.Delete(product, _cancellationToken);

        createdProduct = await productRepository.Get(product.Id, _cancellationToken);

        createdProduct.Should().BeNull();
    }
}