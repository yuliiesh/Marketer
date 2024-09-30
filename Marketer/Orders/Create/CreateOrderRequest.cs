using System;
using System.Collections.Generic;
using Marketer.Data.Models;

namespace Marketer.Orders.Create;

public class CreateOrderRequest
{
    public DateTime CreationDate { get; set; }
    public ICollection<ProductModel> Products { get; set; }
    public Guid CustomerId { get; set; }
}