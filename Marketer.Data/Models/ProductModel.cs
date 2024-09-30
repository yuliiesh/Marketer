using System.ComponentModel.DataAnnotations.Schema;

namespace Marketer.Data.Models;

public class ProductModel : ModelBase
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}