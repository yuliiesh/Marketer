using Marketer.Common.Customers;
using Marketer.Common.Discounts.Create;

namespace Marketer.Common.Discounts;

public interface IDiscountHandler
{
    Task<CreateDiscountResponse> CreateDiscount(CreateDiscountRequest request, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<CustomerDiscountDto>> GetDiscounts(CancellationToken cancellationToken);
}