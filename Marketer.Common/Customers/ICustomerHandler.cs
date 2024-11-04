using Marketer.Common.Customers.Create;
using Marketer.Data.Models;

namespace Marketer.Common.Customers;

public interface ICustomerHandler
{
    Task<List<CustomerDto>> GetAll(CancellationToken cancellationToken);
    Task<CreateCustomerResponse> Create(CreateCustomerRequest request, CancellationToken cancellationToken);
    Task Delete(CustomerModel customer, CancellationToken cancellationToken);
}