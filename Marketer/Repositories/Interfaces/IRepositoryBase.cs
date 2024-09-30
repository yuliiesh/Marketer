using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Marketer.Data.Models;

namespace Marketer.Repositories.Interfaces;

public interface IRepositoryBase<TModel> where TModel : ModelBase
{
    Task<IReadOnlyCollection<TModel>> GetAll(CancellationToken cancellationToken);
    Task<TModel> Get(Guid id, CancellationToken cancellationToken);
    Task Add(TModel model, CancellationToken cancellationToken);
    Task Update(TModel model, CancellationToken cancellationToken);
    Task Delete(TModel model, CancellationToken cancellationToken);
}