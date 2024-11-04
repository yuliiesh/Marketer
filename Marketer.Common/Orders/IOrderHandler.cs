using Marketer.Common.Orders.Create;

namespace Marketer.Common.Orders;

public interface IOrderHandler
{
    Task<CreateOrderResponse> CreateOrder(CreateOrderRequest createOrderRequest, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<OrderDto>> GetOrders(Guid customerId, CancellationToken cancellationToken);
}