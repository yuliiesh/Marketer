using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Marketer.Data.Models;

namespace Marketer.Repositories.Interfaces;

public interface IDiscountRepository : IRepositoryBase<DiscountModel>
{
    Task<IReadOnlyCollection<DiscountModel>> GetWithCustomers(CancellationToken cancellationToken);
}