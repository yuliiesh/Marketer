using Marketer.Authorization;
using Marketer.Authorization.Login;
using Marketer.Authorization.Registration;
using Marketer.Customers;
using Marketer.Customers.Create;
using Marketer.Customers.Read;
using Marketer.Customers.Select;
using Marketer.Discounts;
using Marketer.Discounts.Create;
using Marketer.Discounts.Read;
using Marketer.Orders;
using Marketer.Orders.Create;
using Marketer.Repositories;
using Marketer.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Marketer;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddActions(this IServiceCollection services)
    {
        services.AddTransient<LoginAction>();
        services.AddTransient<RegistrationAction>();
        services.AddTransient<CreateCustomerAction>();
        services.AddTransient<CreateOrderAction>();
        services.AddTransient<ReadCustomersAction>();
        services.AddTransient<CreateDiscountAction>();
        services.AddTransient<SelectCustomerAction>();
        services.AddTransient<ReadCustomerDiscountsAction>();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        return services;
    }

    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<ILoginHandler, LoginHandler>();
        services.AddScoped<ICustomerHandler, CustomerHandler>();
        services.AddScoped<IOrderHandler, OrderHandler>();
        services.AddScoped<IDiscountHandler, DiscountHandler>();
        return services;
    }
}