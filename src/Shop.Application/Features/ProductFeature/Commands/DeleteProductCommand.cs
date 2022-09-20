using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Exceptions;
using Shop.Application.Features.ProductFeature.Models;
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
        var product = new Product() {Id = command.Id};
        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}