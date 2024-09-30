using System;
using System.Threading;
using System.Threading.Tasks;
using Marketer.Data.Models;
using Marketer.Discounts.Create;
using Marketer.Repositories.Interfaces;

namespace Marketer.Discounts;

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
}