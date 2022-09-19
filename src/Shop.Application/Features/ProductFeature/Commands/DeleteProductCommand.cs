using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Exceptions;
using Shop.Application.Interfaces;

namespace Shop.Application.Features.ProductFeature.Commands;

public record DeleteProductCommand(int Id) : IRequest;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {  
        // Find the item and delete
        var existedOne = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == command.Id, cancellationToken);

        if (existedOne == null)
        {
            throw new BadRequest("Product not found");
        }

        _context.Products.Remove(existedOne);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}