using Marketer.Common.Customers.Create;
using Marketer.Common.Discounts;
using Marketer.Data.Models;
using Marketer.Data.Repositories.Interfaces;

namespace Marketer.Common.Customers;

public class CustomerHandler : ICustomerHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IDiscountHandler _discountHandler;

    public CustomerHandler(
        ICustomerRepository customerRepository,
        IOrderRepository orderRepository,
        IDiscountHandler discountHandler)
    {
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
        _discountHandler = discountHandler;
    }

    public async Task<CreateCustomerResponse> Create(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = new CustomerModel
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Age = request.Age,
            Id = Guid.NewGuid(),
        };

        await _customerRepository.Add(customer, cancellationToken);

        return new CreateCustomerResponse
        {
            Customer = customer
        };
    }

    public async Task Delete(CustomerModel customer, CancellationToken cancellationToken)
    {
        var customerWithOrders = await _customerRepository.GetWithOrders(customer.Id, cancellationToken);

        var orders = customerWithOrders.Orders;

        foreach (var order in orders)
        {
            await _orderRepository.Delete(order, cancellationToken);
        }

        await _customerRepository.Delete(customer, cancellationToken);
    }

    public async Task<List<CustomerDto>> GetAll(CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllWithOrders(cancellationToken);
        var discounts = (await _discountHandler.GetDiscounts(cancellationToken)).ToDictionary(x => x.Customer.Id, x => x.Discount);

        return customers.Select(customer =>
        {
            var ordersTotalPrice = customer.Orders?.Sum(o => o.TotalPrice) ?? 0;
            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Age = customer.Age,
                OrdersCount = customer.Orders?.Count ?? 0,
                ProductsTotal = customer.Orders?.SelectMany(o => o.Products).Count() ?? 0,
                TotalPriceWithDiscount = ordersTotalPrice *
                                         (1 - (discounts.TryGetValue(customer.Id, out var discount) ? discount / 100m : 0)),
            };
        }).ToList();
    }
}