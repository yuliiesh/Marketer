using Marketer.Data.Models;

namespace Marketer.Data.Repositories.Interfaces;

public interface IProductRepository : IRepositoryBase<ProductModel>
{
    Task Add(IEnumerable<ProductModel> products, CancellationToken cancellationToken);
}