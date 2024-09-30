using Bogus;
using Marketer;
using Marketer.Data;
using Marketer.Data.Models;
using Marketer.Menu;
using Marketer.Orders;
using Marketer.Orders.Create;
using Marketer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

serviceCollection.AddDbContextFactory<ApplicationDbContext>(options
    => options.UseSqlServer(@"Server=localhost\sqlexpress;Database=marketer;Trusted_Connection=True;Integrated Security=SSPI;TrustServerCertificate=True;"));

serviceCollection.AddRepositories()
    .AddActions()
    .AddHandlers();
serviceCollection.AddSingleton<MenuBuilder>();
serviceCollection.AddKeyedSingleton<IMenu, Menu>("MainMenu",(provider, _) =>
{
    var builder = provider.GetRequiredService<MenuBuilder>();
    var items = builder.Build();
    return new Menu(items);
});

serviceCollection.AddKeyedSingleton<IMenu, Menu>("AuthorizationMenu", (provider, _) =>
{
    var builder = provider.GetRequiredService<MenuBuilder>();
    var items = builder.BuildRegistrationItems();
    return new Menu(items);
});

var provider = serviceCollection.BuildServiceProvider();

var menu = provider.GetRequiredKeyedService<IMenu>("AuthorizationMenu");

try
{
    await menu.Display();
}
catch (Exception e)
{
    Console.WriteLine(e);
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}

