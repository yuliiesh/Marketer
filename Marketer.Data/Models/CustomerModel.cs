namespace Marketer.Data.Models;

public class CustomerModel : ModelBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public ICollection<OrderModel> Orders { get; set; }
}