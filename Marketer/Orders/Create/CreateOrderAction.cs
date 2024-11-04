using Marketer.Common.Orders.Create;
using Marketer.Data.Models;
using Marketer.Data.Repositories.Interfaces;

namespace Marketer.Orders.Create;

public class CreateOrderAction
{
    private readonly ICustomerRepository _customerRepository;

    public CreateOrderAction(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public CreateOrderRequest Invoke()
    {
        Console.WriteLine("Create order");
        var id = Task.Run(SelectCustomer).Result;

        Console.WriteLine("Number of products: ");
        int numberOfProducts;

        while (!int.TryParse(Console.ReadLine(), out numberOfProducts))
        {
            Console.WriteLine("Please enter a valid number");
        }

        var products = CreateProducts(numberOfProducts);

        return new CreateOrderRequest
        {
            CustomerId = id,
            CreationDate = DateTime.Now,
            Products = products,
        };
    }

    private async Task<Guid> SelectCustomer()
    {
        Console.WriteLine("All customers: ");
        var customers = await _customerRepository.GetAll(CancellationToken.None);
        var i = 0;
        foreach (var customer in customers)
        {
            Console.WriteLine($"{++i}. {customer.FirstName} {customer.LastName}");
        }

        Console.WriteLine("Select customer: ");
        var selectedCustomer = Console.ReadLine();
        var customerNumber = 0;

        while (!int.TryParse(selectedCustomer, out customerNumber) || customerNumber < 1 ||
               customerNumber > customers.Count)
        {
            Console.WriteLine("Please enter a valid number");
            selectedCustomer = Console.ReadLine();
        }

        return customers.ElementAtOrDefault(customerNumber - 1)?.Id ?? Guid.Empty;
    }

    private ICollection<ProductModel> CreateProducts(int numberOfProducts)
    {
        var products = new List<ProductModel>(numberOfProducts);

        for (var i = 0; i < numberOfProducts; i++)
        {
            Console.WriteLine($"Product #{i + 1}");
            Console.WriteLine("Product name: ");
            var name = Console.ReadLine();
            Console.WriteLine("Product price: ");
            decimal price;
            while (!decimal.TryParse(Console.ReadLine(), out price))
            {
                Console.WriteLine("Please enter a valid price");
            }

            products.Add(new ProductModel
            {
                Name = name,
                Id = Guid.NewGuid(),
                Price = price,
            });
        }
        return products;
    }
}