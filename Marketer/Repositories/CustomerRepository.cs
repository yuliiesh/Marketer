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

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<CustomerModel>> GetAll(CancellationToken cancellationToken)
    {
        return await _context
            .Customers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<CustomerModel> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context
            .Customers
            .AsNoTracking()
            .Where(customer => customer.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task Add(CustomerModel model, CancellationToken cancellationToken)
    {
        await _context.Customers.AddAsync(model, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(CustomerModel model, CancellationToken cancellationToken)
    {
        _context.Customers.Update(model);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(CustomerModel model, CancellationToken cancellationToken)
    {
        _context.Customers.Remove(model);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<CustomerModel>> GetAllWithOrders(CancellationToken cancellationToken)
    {
        return await _context
            .Customers
            .Include(customer => customer.Orders)
            .ThenInclude(order => order.Products)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<CustomerModel> GetWithOrders(Guid id, CancellationToken cancellationToken)
    {
        return await _context
            .Customers
            .Include(customer => customer.Orders)
            .ThenInclude(order => order.Products)
            .AsNoTracking()
            .FirstOrDefaultAsync(customer => customer.Id == id, cancellationToken);
    }
}