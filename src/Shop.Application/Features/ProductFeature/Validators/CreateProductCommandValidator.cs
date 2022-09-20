using FluentValidation;
using Shop.Application.Features.ProductFeature.Commands;

namespace Shop.Application.Features.ProductFeature.Validators;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.NumberOfItem).GreaterThanOrEqualTo(0);
    }
}