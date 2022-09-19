using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Exceptions;
using Shop.Application.Features.ProductFeature.Dtos;
using Shop.Application.Features.ProductFeature.Models;
using Shop.Application.Interfaces;

namespace Shop.Application.Features.ProductFeature.Commands;

public record UpdateProductCommand(int Id, UpdateProductRequestDto Dto) : IRequest<ProductResponseDto>;

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
        var dto = command.Dto;

        if (command.Id != dto.Id)
        {
            throw new BadRequest("Product update command, request Id and Dto Id are mismatched.");
        }

        // Make sure the item is existed
        var existedOne = await _context.Products.AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == dto.Id, cancellationToken);

        if (existedOne == null)
        {
            throw new BadRequest("Product not found");
        }

        var updatedOne = _mapper.Map<Product>(dto);
        updatedOne.UpdateProductStatus();
        _context.Products.Update(updatedOne);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProductResponseDto>(updatedOne);
    }
}