using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Marketer.Data.Models;

namespace Marketer.Repositories.Interfaces;

public interface ICustomerRepository : IRepositoryBase<CustomerModel>
{
    Task<IReadOnlyCollection<CustomerModel>> GetAllWithOrders(CancellationToken cancellationToken);
    Task<CustomerModel> GetWithOrders(Guid id, CancellationToken cancellationToken);
}