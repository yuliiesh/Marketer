using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketer.Data.Models;

public class OrderModel : ModelBase
{
    public DateTime CreationDate { get; set; }
    public ICollection<ProductModel> Products { get; set; }
    public decimal TotalPrice { get; set; }
}