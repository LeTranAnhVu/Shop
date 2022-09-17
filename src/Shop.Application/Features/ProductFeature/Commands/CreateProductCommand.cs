using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Features.ProductFeature.Dtos;
using Shop.Application.Features.ProductFeature.Models;
using Shop.Application.Interfaces;
using Shop.Application.Models;

namespace Shop.Application.Features.ProductFeature.Commands;

public record CreateProductCommand(CreateProductRequestDto RequestDto) : IRequest<ProductResponseDto>;

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
        var dto = command.RequestDto;
        var product = _mapper.Map<Product>(dto);
        product.Import(null);
        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ProductResponseDto>(product);
    }
}