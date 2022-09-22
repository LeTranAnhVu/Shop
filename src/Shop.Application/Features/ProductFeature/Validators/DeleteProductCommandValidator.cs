using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Application.Features.ProductFeature.Commands;

namespace Shop.Application.Features.ProductFeature.Validators;

public class DeleteProductCommandValidator: AbstractValidator<DeleteProductCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        
        RuleFor(x => x.Id).NotNull().MustAsync(async (id, cancellationToken) =>
        {
            var p = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            return p != null;
        }).WithMessage("Product not found.");
    }
}