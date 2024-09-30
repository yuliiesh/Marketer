using Marketer.ConsoleHelpers;
using Marketer.Data.Models;
using Marketer.Repositories.Interfaces;

namespace Marketer.Customers.Read;

public class ReadCustomersAction
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IDiscountRepository _discountRepository;

    private CustomerModel selectedCustomer;

    private IReadOnlyCollection<CustomerModel> customers;
    private IEnumerable<CustomerModel> filteredCustomers;

    private int currentSelection;
    private int headerSelectionIndex;

    private bool sortingMode;

    private Dictionary<int, Func<CustomerModel, object>> SortingMethods = new()
    {
        { 0, c => c.FirstName },
        { 1, c => c.LastName },
        { 2, c => c.Age.ToString() },
        { 3, c => c.Orders.Count },
        { 4, c => c.Orders.SelectMany(o => o.Products).Count() }
    };

    private HashSet<int> sortedHeaders = [];

    private string _searchCriteria;

    public ReadCustomersAction(ICustomerRepository customerRepository, IDiscountRepository discountRepository)
    {
        _customerRepository = customerRepository;
        _discountRepository = discountRepository;
    }

    public async Task Invoke(CancellationToken cancellationToken)
    {
        customers = await _customerRepository.GetAllWithOrders(cancellationToken);
        var customersCount = customers.Count;

        ConsoleKeyInfo keyInfo;
        do
        {
            PrintHeaderMenu();
            Console.WriteLine("Press (S) to search");
            Console.WriteLine("Press (F) to enter sort mode");

            if (sortingMode)
            {
                Console.WriteLine("Press alt + F to exit sorting mode");
            }

            if (filteredCustomers is not null)
            {
                Console.WriteLine();
                Console.WriteLine($"Search criteria: {_searchCriteria}");
                Console.WriteLine();
                Console.WriteLine("Press alt + S to remove search");
            }

            ShowCustomers(filteredCustomers ?? customers);

            keyInfo = Console.ReadKey(true);
            HandleInput(keyInfo, customersCount);
        } while (keyInfo.Key != ConsoleKey.Escape);
    }

    private static void PrintHeaderMenu()
    {
        Console.Clear();
        Console.WriteLine("Customers");
        Console.WriteLine("Press ESC to exit");
    }

    private void ShowCustomers(IEnumerable<CustomerModel> customers)
    {
        var firstNameHeader = "First Name";
        var lastNameHeader = "Last Name     ";
        var ageHeader = "Age";
        var ordersCountHeader = "Orders Count";
        var productsTotalHeader = "Products Total";

        var headers = new List<string> { firstNameHeader, lastNameHeader, ageHeader, ordersCountHeader, productsTotalHeader };

        var headerString = TableDisplayHelper.BuildHeaderString(headers);

        var splitter = new string('-', headerString.Length);

        Console.WriteLine(splitter);

        Console.Write("| ");
        for (var i = 0; i < headers.Count; i++)
        {
            if (sortingMode && i == headerSelectionIndex)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkBlue;

                Console.Write(headers[i].PadRight(headers[i].Length));

                Console.ResetColor();
                Console.Write(" | ");
            }
            else
            {
                Console.Write(headers[i].PadRight(headers[i].Length) + " | ");
            }
        }

        Console.WriteLine();
        Console.WriteLine(splitter);

        for (var i = 0; i < customers.Count(); i++)
        {
            var customer = customers.ElementAt(i);
            if (!sortingMode && i == currentSelection)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                selectedCustomer = customer;
            }
            else
            {
                Console.ResetColor();
            }

            Console.WriteLine($"| {customer.FirstName.PadRight(firstNameHeader.Length)} " +
                              $"| {customer.LastName.PadRight(lastNameHeader.Length)} " +
                              $"| {customer.Age.ToString().PadRight(ageHeader.Length)} " +
                              $"| {customer.Orders.Count.ToString().PadRight(ordersCountHeader.Length)} " +
                              $"| {customer.Orders.SelectMany(o => o.Products).Count().ToString().PadRight(productsTotalHeader.Length)} |");
        }

        Console.ResetColor();
        Console.WriteLine(splitter);
    }

    private void HandleInput(ConsoleKeyInfo keyInfo, int customersCount)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                currentSelection = currentSelection == 0 ? customersCount - 1 : currentSelection - 1;
                break;
            case ConsoleKey.DownArrow:
                currentSelection = currentSelection >= customersCount - 1 ? 0 : currentSelection + 1;
                break;

            case ConsoleKey.Enter when !sortingMode:
                ShowProducts();
                break;
            case ConsoleKey.Enter when sortingMode:
                Sort();
                break;

            case ConsoleKey.S when keyInfo.Modifiers == ConsoleModifiers.Alt:
                filteredCustomers = null;
                break;
            case ConsoleKey.S:
                Search();
                break;

            case ConsoleKey.LeftArrow when sortingMode:
            {
                headerSelectionIndex = headerSelectionIndex > 0 ? headerSelectionIndex - 1 : 0;
                sortedHeaders.Clear();
            }
                break;
            case ConsoleKey.RightArrow when sortingMode:
            {
                headerSelectionIndex = headerSelectionIndex < 4 ? headerSelectionIndex + 1 : headerSelectionIndex;
                sortedHeaders.Clear();
            }
                break;

            case ConsoleKey.F when keyInfo.Modifiers == ConsoleModifiers.Alt:
                sortingMode = false;
                break;
            case ConsoleKey.F:
                sortingMode = true;
                break;
        }
    }

    private void ShowProducts()
    {
        Console.WriteLine();
        if (selectedCustomer.Orders.Count == 0)
        {
            Console.WriteLine("There are no orders");
            Console.WriteLine("Press any key to get back");
            Console.ReadKey(true);
            return;
        }

        var OrderIdHeader = "Order Id";
        var ProductNameHeader = "Product Name";
        var PriceHeader = "Price";
        var pricePadding = PriceHeader.Length + 5;

        var PriceWithDiscountHeader = "Price with Discount";
        var priceWithDiscountPadding = PriceWithDiscountHeader.Length + 5;
        var headerString = $"| {OrderIdHeader.PadRight(Guid.Empty.ToString().Length)} | {ProductNameHeader} | {PriceHeader.PadRight(pricePadding)} |";

        var discount = _discountRepository.Get(selectedCustomer.Id, CancellationToken.None).GetAwaiter().GetResult();

        if (discount is not null)
        {
            headerString += $" {PriceWithDiscountHeader.PadRight(priceWithDiscountPadding)} |";
        }

        var splitter = new string('-', headerString.Length);

        Console.WriteLine(splitter);
        Console.WriteLine(headerString);
        Console.WriteLine(splitter);

        foreach (var order in selectedCustomer.Orders)
        {
            foreach (var product in order.Products)
            {
                Console.Write($"| {order.Id.ToString().PadRight(OrderIdHeader.Length)} " +
                                  $"| {product.Name.PadRight(ProductNameHeader.Length)} " +
                                  $"| ${product.Price.ToString("0.00").PadRight(pricePadding - 1)} |");
                if (discount is not null)
                {
                    Console.Write($" ${(product.Price * (100 - discount.Discount) / 100).ToString("0.00").PadRight(priceWithDiscountPadding - 1)} |");
                }

                Console.WriteLine();
            }
        }

        Console.WriteLine(splitter);
        Console.WriteLine("Press any key to get back");
        Console.ReadKey(true);
    }

    private void Search()
    {
        Console.Clear();
        PrintHeaderMenu();

        Console.Write("Enter search criteria: ");

        _searchCriteria = Console.ReadLine() ?? string.Empty;

        filteredCustomers = customers.Where(customer =>
            customer.FirstName.Contains(_searchCriteria, StringComparison.OrdinalIgnoreCase)
            || customer.LastName.Contains(_searchCriteria, StringComparison.OrdinalIgnoreCase));
    }

    private void Sort()
    {
        var isSortedColumn = sortedHeaders.Contains(headerSelectionIndex);

        if (filteredCustomers is not null)
        {
            if (!isSortedColumn)
            {
                filteredCustomers = filteredCustomers.OrderBy(SortingMethods[headerSelectionIndex]).ToList();
                sortedHeaders.Add(headerSelectionIndex);
            }
            else
            {
                filteredCustomers = filteredCustomers.OrderByDescending(SortingMethods[headerSelectionIndex]).ToList();
                sortedHeaders.Remove(headerSelectionIndex);
            }

            return;
        }

        if (!isSortedColumn)
        {
            customers = customers.OrderBy(SortingMethods[headerSelectionIndex]).ToList();
            sortedHeaders.Add(headerSelectionIndex);
        }
        else
        {
            customers = customers.OrderByDescending(SortingMethods[headerSelectionIndex]).ToList();
            sortedHeaders.Remove(headerSelectionIndex);
        }
    }
}