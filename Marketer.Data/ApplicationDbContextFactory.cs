using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Marketer.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(
            @"Server=localhost\sqlexpress;Database=marketer;Trusted_Connection=True;Integrated Security=SSPI;TrustServerCertificate=True;");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}