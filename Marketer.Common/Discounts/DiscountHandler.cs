using Marketer.Common.Customers;
using Marketer.Common.Discounts.Create;
using Marketer.Data.Models;
using Marketer.Data.Repositories.Interfaces;

namespace Marketer.Common.Discounts;

public class DiscountHandler : IDiscountHandler
{
    private readonly IDiscountRepository _discountRepository;

    public DiscountHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task<CreateDiscountResponse> CreateDiscount(CreateDiscountRequest request, CancellationToken cancellationToken)
    {
        if (request.Discount is > 100 or < 0)
        {
            throw new ArgumentException("Discount must be between 0 and 100.", nameof(request.Discount));
        }

        var discount = new DiscountModel
        {
            Id = Guid.NewGuid(),
            Discount = request.Discount,
            CustomerId = request.CustomerId,
        };

        await _discountRepository.Add(discount, cancellationToken);

        return new CreateDiscountResponse
        {
            Discount = request.Discount,
            CustomerId = request.CustomerId,
        };
    }

    public async Task<IReadOnlyCollection<CustomerDiscountDto>> GetDiscounts(CancellationToken cancellationToken)
    {
        var discounts = await _discountRepository.GetWithCustomers(cancellationToken);

        return discounts.Select(discount => new CustomerDiscountDto
        {
            Discount = discount.Discount,
            Customer = new CustomerDto()
            {
                Id = discount.Customer.Id,
                FirstName = discount.Customer.FirstName,
                LastName = discount.Customer.LastName,
            }
        }).ToList();
    }
}