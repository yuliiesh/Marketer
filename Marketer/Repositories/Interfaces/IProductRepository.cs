using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Marketer.Data.Models;

namespace Marketer.Repositories.Interfaces;

public interface IProductRepository : IRepositoryBase<ProductModel>
{
    Task Add(IEnumerable<ProductModel> products, CancellationToken cancellationToken);
}