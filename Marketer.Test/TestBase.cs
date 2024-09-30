using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Marketer.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Marketer.Test;

public abstract class TestBase : IDisposable
{
    protected readonly ApplicationDbContext _context;
    protected IServiceProvider _serviceProvider;

    protected IServiceCollection _serviceCollection;

    private static readonly CancellationTokenSource _cts = new();
    protected static readonly CancellationToken _cancellationToken = _cts.Token;

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

        _serviceCollection = new ServiceCollection();
        _serviceCollection.AddSingleton(_ => _context);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    protected void ClearTracker()
    {
        _context.ChangeTracker.Clear();
    }
}