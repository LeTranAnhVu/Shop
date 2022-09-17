using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.CQRS.Queries;
using Shop.Application.Features.ProductFeature.Commands;
using Shop.Application.Features.ProductFeature.Dtos;
using Shop.Application.Features.ProductFeature.Models;
using Shop.Application.Models;

namespace Shop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly ISender _sender;

    public ProductsController(ILogger<ProductsController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet]
    public async Task<IEnumerable<ProductResponseDto>> Get()
    {
        _logger.LogInformation("Hit {} request {}", nameof(ProductsController), nameof(Get));
        return await _sender.Send(new GetAllProductsQuery());
    }

    [HttpPost]
    public async Task<Product> Import(CreateProductRequestDto requestDto)
    {
        _logger.LogInformation("Hit {} request {}", nameof(ProductsController), nameof(Import));

        return await _sender.Send(new CreateProductCommand(requestDto));
    }
    
    [HttpPut("{id}")]
    public async Task<Product> Update(int id, UpdateProductRequestDto requestDto)
    {
        _logger.LogInformation("Hit {} request {}", nameof(ProductsController), nameof(Update));

        return await _sender.Send(new UpdateProductCommand(id, requestDto));
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Hit {} request {}", nameof(ProductsController), nameof(Delete));

        await _sender.Send(new DeleteProductCommand(id));
        return NoContent();
    }
}