using System.ComponentModel.DataAnnotations.Schema;

namespace Marketer.Data.Models;

public class DiscountModel : ModelBase
{
    public Guid CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public CustomerModel Customer { get; set; }

    public int Discount { get; set; }
}