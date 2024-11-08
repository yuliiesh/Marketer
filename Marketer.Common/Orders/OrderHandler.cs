﻿using Marketer.Common.Orders.Create;
using Marketer.Data.Models;
using Marketer.Data.Repositories.Interfaces;

namespace Marketer.Common.Orders;

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
            CustomerModelId = customer.Id
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

    public async Task<IReadOnlyCollection<OrderDto>> GetOrders(Guid customerId, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAll(customerId, cancellationToken);

        return orders.Select(x => new OrderDto
        {
            Id = x.Id,
            CreationDate = x.CreationDate,
            Products = x.Products
        }).ToList();
    }
}