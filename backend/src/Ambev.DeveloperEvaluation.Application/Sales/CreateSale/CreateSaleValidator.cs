using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleValidator()
        {
            RuleFor(x => x.SaleNumber).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Items).NotEmpty().WithMessage("Sale must contain at least one item.");
            // etc. for other fields as needed
        }
    }
}
