using System;

namespace Marketer.Authorization.Login;

public class LoginAction
{
    public LoginRequest Invoke()
    {
        Console.WriteLine("Enter username: ");
        var username = Console.ReadLine();

        Console.WriteLine("Enter password: ");
        var password = Console.ReadLine();

        return new LoginRequest { Username = username, Password = password };
    }
}