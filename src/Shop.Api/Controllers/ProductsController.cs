using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Features.ProductFeature.Commands;
using Shop.Application.Features.ProductFeature.Dtos;
using Shop.Application.Features.ProductFeature.Models;
using Shop.Application.Features.ProductFeature.Queries;

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
    public async Task<Product> Post(CreateProductRequestDto requestDto)
    {
        _logger.LogInformation("Hit {} request {}", nameof(ProductsController), nameof(Post));

        return await _sender.Send(new CreateProductCommand(requestDto));
    }

    [HttpPut("{id}")]
    public async Task<Product> Put(int id, UpdateProductRequestDto requestDto)
    {
        _logger.LogInformation("Hit {} request {}", nameof(ProductsController), nameof(Put));

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