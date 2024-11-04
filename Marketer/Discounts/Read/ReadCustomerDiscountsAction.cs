using Marketer.ConsoleHelpers;
using Marketer.Data.Repositories.Interfaces;

namespace Marketer.Discounts.Read;

public class ReadCustomerDiscountsAction
{
    private readonly IDiscountRepository _discountRepository;

    public ReadCustomerDiscountsAction(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task Invoke(CancellationToken cancellationToken)
    {
        var discounts = await _discountRepository.GetWithCustomers(cancellationToken);

        var headers = new List<string> { "First Name", "Last Name", "Discount" };

        var headerString = TableDisplayHelper.BuildHeaderString(headers);
        TableDisplayHelper.DisplayHeadersAndSplitter(headers);

        foreach (var discount in discounts)
        {
            var customer = discount.Customer;
            var rowValues = new List<string>
            {
                customer.FirstName,
                customer.LastName,
                (discount.Discount / 100f).ToString("# %")
            };
            TableDisplayHelper.DisplayRow(rowValues, headers);
        }

        Console.WriteLine(TableDisplayHelper.BuildSplitter(headerString));

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}