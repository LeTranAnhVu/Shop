using Shop.Application.Features.ProductFeature.Models;
using Shop.Application.Models.Enums;

namespace Shop.Application.Features.ProductFeature.Dtos;


// Requests
public class CreateProductRequestDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int NumberOfItem { get; set; }
}

public class UpdateProductRequestDto : CreateProductRequestDto
{
    public int Id { get; set; }
    public ProductStatus ProductStatus { get; set; }
}

// Response
public class ProductResponseDto: Product
{
}
