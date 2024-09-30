﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Marketer.Authorization.Login;
using Marketer.Authorization.Registration;
using Marketer.Data.Models;
using Marketer.Repositories.Interfaces;

namespace Marketer.Authorization;

public class LoginHandler : ILoginHandler
{
    private readonly IUserRepository _userRepository;

    public LoginHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<LoginResponse> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(request.Username, request.Password, cancellationToken);
        if (user is null)
        {
            return new LoginResponse
            {
                Success = false,
                ErrorMessage = "Wrong username or password"
            };
        }

        return new LoginResponse
        {
            Success = true,
        };
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(request.Username, cancellationToken);

        if (user is null)
        {
            user = new UserModel
            {
                Id = Guid.NewGuid(),
                Password = request.Password,
                UserName = request.Username,
            };
            await _userRepository.Add(user, cancellationToken);

            return new RegistrationResponse
            {
                Success = true,
                User = user,
            };
        }

        return new RegistrationResponse
        {
            Success = false,
            ErrorMessage = "User already exists",
        };
    }
}