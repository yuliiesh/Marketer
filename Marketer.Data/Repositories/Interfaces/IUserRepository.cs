using Marketer.Data.Models;

namespace Marketer.Data.Repositories.Interfaces;

public interface IUserRepository : IRepositoryBase<UserModel>
{
    Task<UserModel> Get(string username, string password, CancellationToken cancellationToken);
    Task<UserModel> Get(string username, CancellationToken cancellationToken);
}