using Marketer.Data;
using Marketer.Data.Models;
using Marketer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Marketer.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<UserModel>> GetAll(CancellationToken cancellationToken)
    {
        return await _context
            .Users
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<UserModel> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context
            .Users
            .AsNoTracking()
            .Where(user => user.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task Add(UserModel model, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(model, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(UserModel model, CancellationToken cancellationToken)
    {
        _context.Users.Update(model);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(UserModel model, CancellationToken cancellationToken)
    {
        _context.Users.Remove(model);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserModel> Get(string username, string password, CancellationToken cancellationToken)
    {
        return await _context.Users.Where(user => user.UserName == username && user.Password == password).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<UserModel> Get(string username, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.UserName == username, cancellationToken);
    }
}