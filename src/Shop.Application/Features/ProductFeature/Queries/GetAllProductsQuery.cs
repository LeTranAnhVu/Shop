using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Features.ProductFeature.Dtos;
using Shop.Application.Features.ProductFeature.Models;
using Shop.Application.Interfaces;
using Shop.Application.Models;

namespace Shop.Application.CQRS.Queries;

public record GetAllProductsQuery : IRequest<IEnumerable<ProductResponseDto>>;

public class GetAllProductsHandler: IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllProductsHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ProductResponseDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Products.ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }
}