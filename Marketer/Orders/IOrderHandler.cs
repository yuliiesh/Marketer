using System.Threading;
using System.Threading.Tasks;
using Marketer.Orders.Create;

namespace Marketer.Orders;

public interface IOrderHandler
{
    Task<CreateOrderResponse> CreateOrder(CreateOrderRequest createOrderRequest, CancellationToken cancellationToken);
}