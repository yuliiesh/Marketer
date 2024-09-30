namespace Marketer.Discounts.Create;

public class CreateDiscountRequest
{
    public int Discount { get; set; }

    public Guid CustomerId { get; set; }
}