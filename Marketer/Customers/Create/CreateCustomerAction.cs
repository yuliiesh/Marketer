namespace Marketer.Customers.Create;

public class CreateCustomerAction
{
    public CreateCustomerRequest Invoke()
    {
        Console.WriteLine("Create customer");

        var name = string.Empty;
        var lastName = string.Empty;

        while (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Customer first name: ");
            name = Console.ReadLine();
        }

        while (string.IsNullOrEmpty(lastName))
        {
            Console.WriteLine("Customer last name: ");
            lastName = Console.ReadLine();
        }

        var ageString = string.Empty;
        int age = 0;
        while (!int.TryParse(ageString, out age) || age <= 0)
        {
            Console.WriteLine("Customer age: ");
            ageString = Console.ReadLine();
        }

        return new CreateCustomerRequest
        {
            FirstName = name,
            LastName = lastName,
            Age = age
        };
    }
}