using FluentAssertions;
using Marketer.Data.Models;
using Marketer.Data.Repositories;
using Marketer.Data.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Marketer.Test.RepositoryTests;

public class UserRepositoryTests : TestBase
{
    public UserRepositoryTests()
    {
        _serviceCollection.AddSingleton<IUserRepository, UserRepository>();
        _serviceProvider = _serviceCollection.BuildServiceProvider();
    }

    [Fact]
    public async Task CreateUser_Success()
    {
        //Arrange
        var user = new UserModel
        {
            Id = Guid.NewGuid(),
            UserName = "test",
            Password = "test",
        };

        var userRepository = _serviceProvider.GetRequiredService<IUserRepository>();

        //Act
        await userRepository.Add(user, _cancellationToken);

        //Assert
        var createdUser = await userRepository.Get(user.Id, _cancellationToken);

        createdUser.Should().NotBeNull();
        createdUser.Id.Should().Be(user.Id);
        createdUser.UserName.Should().Be(user.UserName);
        createdUser.Password.Should().Be(user.Password);
    }

    [Fact]
    public async Task UpdateUser_Success()
    {
        //Create new
        var user = new UserModel
        {
            Id = Guid.NewGuid(),
            UserName = "test2",
            Password = "test",
        };

        var userRepository = _serviceProvider.GetRequiredService<IUserRepository>();

        await userRepository.Add(user, _cancellationToken);

        //Assert  created
        var createdUser = await userRepository.Get(user.Id, _cancellationToken);

        createdUser.Should().NotBeNull();
        createdUser.Id.Should().Be(user.Id);
        createdUser.UserName.Should().Be(user.UserName);
        createdUser.Password.Should().Be(user.Password);

        //Change age
        user.UserName = "test2";
        user.Password = "newpassword";

        //Update
        await userRepository.Update(user, _cancellationToken);

        createdUser = await userRepository.Get(user.Id, _cancellationToken);

        //Assert age updated
        createdUser.UserName.Should().Be("test2");
        createdUser.Password.Should().Be("newpassword");
    }

    [Fact]
    public async Task DeleteUser_Success()
    {
        var user = new UserModel
        {
            Id = Guid.NewGuid(),
            UserName = "test",
            Password = "test",
        };

        var userRepository = _serviceProvider.GetRequiredService<IUserRepository>();

        await userRepository.Add(user, _cancellationToken);

        var createdUser = await userRepository.Get(user.Id, _cancellationToken);

        createdUser.Should().NotBeNull();

        await userRepository.Delete(user, _cancellationToken);

        createdUser = await userRepository.Get(user.Id, _cancellationToken);

        createdUser.Should().BeNull();
    }
}