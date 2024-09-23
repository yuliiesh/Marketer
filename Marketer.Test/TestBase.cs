using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Marketer.Data;

namespace Marketer.Test;

public abstract class TestBase : IDisposable
{
    protected readonly ApplicationDbContext _context;

    private readonly string _connectionString;

    protected TestBase()
    {
        var testDbName = $"TestDb_{Guid.NewGuid()}";

        _connectionString = new SqlConnectionStringBuilder
        {
            DataSource = @"localhost\sqlexpress",
            IntegratedSecurity = true,
            TrustServerCertificate = true,
            InitialCatalog = testDbName
        }.ConnectionString;

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(_connectionString)
            .Options;

        _context = new ApplicationDbContext(options);

        _context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}