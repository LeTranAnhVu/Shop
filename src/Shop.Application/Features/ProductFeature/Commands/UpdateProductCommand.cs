using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Exceptions;
using Shop.Application.Common.Interfaces;
using Shop.Application.Features.ProductFeature.Dtos;
using Shop.Application.Features.ProductFeature.Models;
using Shop.Application.Models.Enums;

namespace Shop.Application.Features.ProductFeature.Commands;

public record UpdateProductCommand : IRequest<ProductResponseDto>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int NumberOfItems { get; set; }
    public ProductStatus ProductStatus { get; set; }
};

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponseDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductResponseDto> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var updatedOne = _mapper.Map<Product>(command);
        updatedOne.UpdateProductStatus();
        _context.Products.Update(updatedOne);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProductResponseDto>(updatedOne);
    }
}