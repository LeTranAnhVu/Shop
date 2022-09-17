using Shop.Application.Models.Enums;

namespace Shop.Application.Models;

public class Product
{
    public int Id { get; set; } 
    public string? Name { get; set; } 
    public string? Description { get; set; } 
    public int NumberOfItem { get; set; }
    public ProductStatus ProductStatus { get; set; }
}