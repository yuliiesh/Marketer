using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Marketer.ConsoleHelpers;
using Marketer.Data.Models;
using Marketer.Repositories.Interfaces;

namespace Marketer.Customers.Select;

public class SelectCustomerAction
{
    private readonly ICustomerRepository _customerRepository;

    private int currentSelection;

    public SelectCustomerAction(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerModel> Invoke(CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAll(cancellationToken);
        var customersCount = customers.Count;

        ConsoleKeyInfo keyInfo;
        do
        {
            Console.Clear();
            ShowCustomers(customers);
            keyInfo = Console.ReadKey(true);
            HandleInput(keyInfo, customersCount);
        } while (keyInfo.Key != ConsoleKey.Enter);

        return customers.ElementAt(currentSelection);
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
        }
    }

    private void ShowCustomers(IReadOnlyCollection<CustomerModel> customers)
    {
        var headers = new List<string> { "First Name", "Last Name" };

        var headerString = TableDisplayHelper.BuildHeaderString(headers);

        TableDisplayHelper.DisplayHeadersAndSplitter(headers);
        for (var i = 0; i < customers.Count; i++)
        {
            var customer = customers.ElementAt(i);
            if (i == currentSelection)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
            else
            {
                Console.ResetColor();
            }

            var rowValues = new List<string>
            {
                customer.FirstName,
                customer.LastName
            };
            TableDisplayHelper.DisplayRow(rowValues, headers);
        }

        Console.ResetColor();
        Console.WriteLine(TableDisplayHelper.BuildSplitter(headerString));
    }
}