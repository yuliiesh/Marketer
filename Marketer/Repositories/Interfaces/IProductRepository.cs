using Marketer.Data.Models;

namespace Marketer.Repositories.Interfaces;

public interface IProductRepository : IRepositoryBase<ProductModel>
{
    Task Add(IEnumerable<ProductModel> products, CancellationToken cancellationToken);
}