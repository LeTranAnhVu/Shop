using Shop.Application.Features.ProductFeature.Dtos;
using Shop.Application.Models.Enums;

namespace Shop.Application.Features.ProductFeature.Models;

public class Product
{
    public int Id { get; set; } 
    public string? Name { get; set; } 
    public string? Description { get; set; } 
    public int NumberOfItem { get; set; }
    public ProductStatus ProductStatus { get; set; }

    public virtual string ProductStatusText
    {
        get
        {
            switch (ProductStatus)
            {
                case ProductStatus.InStock:
                    return "In stock";
                case ProductStatus.OutOfStock:
                    return "Out of stock";
                default:
                    return "Unknown";
            }
        }
    }

    public void Import(Product? existedProduct = null)
    {
        var existedItems = 0;

        if (existedProduct != null)
        {
            existedItems = existedProduct.NumberOfItem;
        }

        NumberOfItem += existedItems;
        ProductStatus = NumberOfItem > 0 ? ProductStatus.InStock : ProductStatus.OutOfStock;
    }
}