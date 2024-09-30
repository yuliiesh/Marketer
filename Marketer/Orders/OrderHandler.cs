using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Marketer.Data.Models;
using Marketer.Orders.Create;
using Marketer.Repositories.Interfaces;

namespace Marketer.Orders;

public class OrderHandler : IOrderHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderHandler(ICustomerRepository customerRepository, IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest createOrderRequest, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.Get(createOrderRequest.CustomerId, cancellationToken);

        var order = new OrderModel
        {
            Id = Guid.NewGuid(),
            CreationDate = createOrderRequest.CreationDate,
            Products = createOrderRequest.Products,
            TotalPrice = createOrderRequest.Products.Sum(p => p.Price),
        };

        await _productRepository.Add(order.Products, cancellationToken);

        await _orderRepository.Add(order, cancellationToken);

        customer.Orders ??= [];
        customer.Orders.Add(order);
        await _customerRepository.Update(customer, cancellationToken);

        return new CreateOrderResponse
        {
            OrderId = order.Id,
            TotalPrice = order.TotalPrice,
            Products = order.Products,
            CustomerId = customer.Id,
        };
    }
}