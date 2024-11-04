namespace Marketer.Common.Customers;

public class CustomerDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public int OrdersCount { get; set; }
    public int ProductsTotal { get; set; }
    public decimal TotalPriceWithDiscount { get; set; }

    public string FullName => $"{FirstName} {LastName}";
}