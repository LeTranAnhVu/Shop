using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Features.ProductFeature.Commands;
using Shop.Application.Interfaces;

namespace Shop.Application.Features.ProductFeature.Validators;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(x => x.Name).NotEmpty().MustAsync(async (name, cancellationToken) =>
        {
            var p = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Name == name, cancellationToken);
            return p == null;
        }).WithMessage("Product's name existed.");
        RuleFor(x => x.NumberOfItems).GreaterThanOrEqualTo(0);
    }
}