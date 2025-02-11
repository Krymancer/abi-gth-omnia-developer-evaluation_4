using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
    {
        public UpdateSaleRequestValidator()
        {
            RuleFor(x => x.SaleNumber).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Items).NotEmpty().WithMessage("Must have at least one sale item.");
        }
    }
}
