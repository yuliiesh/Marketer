using Marketer.Customers.Create;
using Marketer.Data.Models;

namespace Marketer.Customers;

public interface ICustomerHandler
{
    Task<CreateCustomerResponse> Create(CreateCustomerRequest request, CancellationToken cancellationToken);
    Task Delete(CustomerModel customer, CancellationToken cancellationToken);
}