using Marketer.Customers.Create;
using Marketer.Data.Models;
using Marketer.Repositories.Interfaces;

namespace Marketer.Customers;

public class CustomerHandler : ICustomerHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public CustomerHandler(ICustomerRepository customerRepository, IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
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
}