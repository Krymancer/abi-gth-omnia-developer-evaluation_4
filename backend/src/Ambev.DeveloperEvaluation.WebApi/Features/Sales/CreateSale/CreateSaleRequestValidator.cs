using System.Data;
using FluentValidation;
using FluentValidation.Validators;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
            RuleFor(r => r.SaleNumber)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(r => r.Items)
                .NotEmpty().WithMessage("Sale must contain at least one item.");

            RuleForEach(r => r.Items)
                .SetValidator(new CreateSaleItemRequestValidator());            
        }
    }

    internal class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
    {        

        public CreateSaleItemRequestValidator()
        {
            RuleFor(r => r.UnitPrice)
                .GreaterThan(0)
                .WithMessage("Unit price must be greater than zero.");

            RuleFor(r => r.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");
        }
    }
}
