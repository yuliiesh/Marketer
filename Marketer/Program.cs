using Marketer.Data;
using Marketer.Menu;
using Marketer.Repositories;
using Marketer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

serviceCollection.AddDbContextFactory<ApplicationDbContext>(options
    => options.UseSqlServer(@"Server=localhost\sqlexpress;Database=marketer;Trusted_Connection=True;Integrated Security=SSPI;TrustServerCertificate=True;"));

serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();

serviceCollection.AddTransient<MenuBuilder>();
serviceCollection.AddSingleton<IMenu, Menu>(provider =>
{
    var builder = provider.GetRequiredService<MenuBuilder>();
    var items = builder.Build();
    return new Menu(items);
});

var provider = serviceCollection.BuildServiceProvider();

var menu = provider.GetRequiredService<IMenu>();

menu.Display();