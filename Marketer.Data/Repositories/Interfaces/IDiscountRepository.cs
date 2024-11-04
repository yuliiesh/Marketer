using Marketer.Data.Models;

namespace Marketer.Data.Repositories.Interfaces;

public interface IDiscountRepository : IRepositoryBase<DiscountModel>
{
    Task<IReadOnlyCollection<DiscountModel>> GetWithCustomers(CancellationToken cancellationToken);
}