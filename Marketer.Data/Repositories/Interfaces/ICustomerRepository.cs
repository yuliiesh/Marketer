using Marketer.Data.Models;

namespace Marketer.Data.Repositories.Interfaces;

public interface ICustomerRepository : IRepositoryBase<CustomerModel>
{
    Task<IReadOnlyCollection<CustomerModel>> GetAllWithOrders(CancellationToken cancellationToken);
    Task<CustomerModel> GetWithOrders(Guid id, CancellationToken cancellationToken);
}