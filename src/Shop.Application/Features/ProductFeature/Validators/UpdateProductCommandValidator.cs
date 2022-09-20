using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Features.ProductFeature.Commands;
using Shop.Application.Interfaces;

namespace Shop.Application.Features.ProductFeature.Validators;

public class UpdateProductCommandValidator: AbstractValidator<UpdateProductCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        
        RuleFor(x => x.Name).NotEmpty().MustAsync(async (x, name, cancellationToken) =>
        {
            var p = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Name == name && p.Id != x.Id, cancellationToken);
            return p == null;
        }).WithMessage("Updated product's name existed.");        
        
        RuleFor(x => x.Id).NotNull().MustAsync(async (id, cancellationToken) =>
        {
            var p = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            return p != null;
        }).WithMessage("Product not found.");
        
        RuleFor(x => x.NumberOfItems).GreaterThanOrEqualTo(0);
        RuleFor(x => x.ProductStatus).IsInEnum();
    }
}