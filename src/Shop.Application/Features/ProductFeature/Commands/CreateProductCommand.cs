using AutoMapper;
using MediatR;
using Shop.Application.Features.ProductFeature.Dtos;
using Shop.Application.Features.ProductFeature.Models;
using Shop.Application.Interfaces;

namespace Shop.Application.Features.ProductFeature.Commands;

public record CreateProductCommand(string Name, string Description, int NumberOfItem) : IRequest<ProductResponseDto>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponseDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ProductResponseDto> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(command);
        product.UpdateProductStatus();
        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ProductResponseDto>(product);
    }
}