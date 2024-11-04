using FluentAssertions;
using Marketer.Common.Authorization;
using Marketer.Common.Authorization.Login;
using Marketer.Data.Models;
using Marketer.Data.Repositories;
using Marketer.Data.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Marketer.Test.LoginHandlerTests;

public class LoginHandlerTests : TestBase
{
    public LoginHandlerTests()
    {
        _serviceCollection.AddSingleton<IUserRepository, UserRepository>();
        _serviceCollection.AddSingleton<ILoginHandler, LoginHandler>();
        _serviceProvider = _serviceCollection.BuildServiceProvider();
    }

    [Fact]
    public async Task Login_UserExits_Success()
    {
        //Arrange user
        await CreateUser();

        var loginRequest = new LoginRequest
        {
            Password = "test",
            Username = "test"
        };

        var loginHandler = _serviceProvider.GetService<ILoginHandler>();
        //Act
        var loginResponse = await loginHandler.Login(loginRequest, _cancellationToken);

        //Assert login success
        loginResponse.Should().NotBeNull();
        loginResponse.Success.Should().BeTrue();
    }

    [Fact]
    public async Task Login_WrongPassword_Fail()
    {
        //Arrange user
        await CreateUser();

        var loginRequest = new LoginRequest
        {
            Password = "test",
            Username = "wrong password"
        };

        var loginHandler = _serviceProvider.GetService<ILoginHandler>();
        //Act
        var loginResponse = await loginHandler.Login(loginRequest, _cancellationToken);

        //Assert login success
        loginResponse.Should().NotBeNull();
        loginResponse.Success.Should().BeFalse();
    }

    private async Task CreateUser()
    {
        var user = new UserModel
        {
            Id = Guid.NewGuid(),
            UserName = "test",
            Password = "test"
        };
        var userRepository = _serviceProvider.GetService<IUserRepository>();
        await userRepository.Add(user, _cancellationToken);
    }
}