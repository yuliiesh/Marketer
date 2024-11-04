using Marketer.Common.Discounts.Create;
using Marketer.Data.Models;

namespace Marketer.Discounts.Create;

public class CreateDiscountAction
{
    public CreateDiscountRequest Invoke(CustomerModel customer)
    {
        Console.WriteLine("Create Discount");

        Console.Write("Enter Discount: ");

        int discount;
        while (!int.TryParse(Console.ReadLine(), out discount) || discount < 0 || discount > 100)
        {
            Console.WriteLine("Invalid Discount. Please try again.");
        }

        return new CreateDiscountRequest
        {
            CustomerId = customer.Id,
            Discount = discount
        };
    }
}