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

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<ProductModel>> GetAll(CancellationToken cancellationToken)
    {
        return await _context
            .Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<ProductModel> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context
            .Products
            .AsNoTracking()
            .Where(product => product.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task Add(ProductModel model, CancellationToken cancellationToken)
    {
        await _context.Products.AddAsync(model, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(ProductModel model, CancellationToken cancellationToken)
    {
        _context.Products.Update(model);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(ProductModel model, CancellationToken cancellationToken)
    {
        _context.Products.Remove(model);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Add(IEnumerable<ProductModel> products, CancellationToken cancellationToken)
    {
        _context.Products.AddRange(products);
        await _context.SaveChangesAsync(cancellationToken);
    }
}