using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Marketer.Data;
using Marketer.Data.Models;
using Marketer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Marketer.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<OrderModel>> GetAll(CancellationToken cancellationToken)
    {
        return await _context
            .Orders
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<OrderModel> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context
            .Orders
            .Include(order => order.Products)
            .AsNoTracking()
            .Where(order => order.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task Add(OrderModel model, CancellationToken cancellationToken)
    {
        await _context.Orders.AddAsync(model, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(OrderModel model, CancellationToken cancellationToken)
    {
        _context.Orders.Update(model);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(OrderModel model, CancellationToken cancellationToken)
    {
        _context.Orders.Remove(model);
        await _context.SaveChangesAsync(cancellationToken);
    }
}