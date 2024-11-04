using Marketer.Data.Models;

namespace Marketer.Common.Orders;

public class OrderDto
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public ICollection<ProductModel> Products { get; set; }
}