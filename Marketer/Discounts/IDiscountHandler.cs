using System.Threading;
using System.Threading.Tasks;
using Marketer.Discounts.Create;

namespace Marketer.Discounts;

public interface IDiscountHandler
{
    Task<CreateDiscountResponse> CreateDiscount(CreateDiscountRequest request, CancellationToken cancellationToken);
}