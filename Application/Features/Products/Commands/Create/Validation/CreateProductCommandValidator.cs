using FluentValidation;

namespace Application.Features.Products.Commands.Create.Validation
{
    public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Title).NotEmpty().NotNull().MaximumLength(200);
            RuleFor(p => p.Description).NotEmpty().MaximumLength(1500);
            RuleFor(p => p.StockQuantity).NotEmpty().GreaterThan(0);

        }
    }
}
