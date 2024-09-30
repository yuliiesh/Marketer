namespace Marketer.Authorization.Registration;

public class RegistrationAction
{
    public RegistrationRequest Invoke()
    {
        Console.WriteLine("Enter your Username:");
        var username = Console.ReadLine();

        Console.WriteLine("Enter your Password:");
        var password = Console.ReadLine();

        Console.WriteLine("Confirm your Password:");
        var passwordConfirm = Console.ReadLine();

        while (password != passwordConfirm)
        {
            Console.WriteLine("Confirm your Password again:");
            passwordConfirm = Console.ReadLine();
        }

        return new RegistrationRequest { Username = username, Password = password };
    }
}