using Marketer.Data.Models;
using Marketer.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Marketer.Data.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly ApplicationDbContext _context;
    public DiscountRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<DiscountModel>> GetAll(CancellationToken cancellationToken)
    {
        return await _context
            .Discounts
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<DiscountModel> Get(Guid customerId, CancellationToken cancellationToken)
    {
       return await _context
           .Discounts
           .AsNoTracking()
           .FirstOrDefaultAsync(discount => discount.CustomerId == customerId, cancellationToken);
    }

    public async Task Add(DiscountModel model, CancellationToken cancellationToken)
    {
        _context.Discounts.Add(model);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(DiscountModel model, CancellationToken cancellationToken)
    {
       _context.Discounts.Update(model);
       await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(DiscountModel model, CancellationToken cancellationToken)
    {
        _context.Discounts.Remove(model);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<DiscountModel>> GetWithCustomers(CancellationToken cancellationToken)
    {
        return await _context.Discounts
            .AsNoTracking()
            .Include(discount => discount.Customer)
            .ToListAsync(cancellationToken);
    }
}