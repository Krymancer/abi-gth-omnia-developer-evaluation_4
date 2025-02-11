using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales
{
    public class GetSalesRequestValidator : AbstractValidator<GetSalesRequest>
    {
        public GetSalesRequestValidator()
        {
            RuleFor(r => r.Page).GreaterThanOrEqualTo(1).WithMessage("Page must be greater than or equal to 1.");
            RuleFor(r => r.Size).GreaterThanOrEqualTo(1).WithMessage("Size must be greater than or equal to 1.");
        }
    }
}
