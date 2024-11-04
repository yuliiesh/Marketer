using Marketer.Data.Models;

namespace Marketer.Data.Repositories.Interfaces;

public interface IOrderRepository : IRepositoryBase<OrderModel>
{
    public Task<IReadOnlyList<OrderModel>> GetAll(Guid customerId, CancellationToken cancellationToken);
}