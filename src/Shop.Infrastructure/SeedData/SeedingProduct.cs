using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shop.Application.Common.Interfaces;
using Shop.Application.Features.ProductFeature.Models;
using Shop.Application.Models.Enums;

namespace Shop.Infrastructure.SeedData;

public static partial class ApplicationDbContextExtensions
{
    public static async Task<IApplicationDbContext> SeedProductsAsync(this IApplicationDbContext context, ILogger<IApplicationDbContext> logger)
    {
        if (!await context.Products.AnyAsync())
        {
            logger.LogInformation("{}: Seeding data to entity {}...", nameof(SeedData), nameof(context.Products));
            var products = new List<Product>()
            {
                new()
                {
                    Id = 1,
                    Name = "Airpod 2",
                    Description =
                        " Put them in your ears and they connect immediately, immersing you in rich, high-quality sound. Just like magic.",
                    ProductStatus = ProductStatus.InStock,
                    NumberOfItems = 50
                },
                new()
                {
                    Id = 2,
                    Name = "Ps4 (Play Station 4)",
                    Description = "Incredible games live on PS4, with 1TB storage.",
                    ProductStatus = ProductStatus.InStock,
                    NumberOfItems = 30
                },
                new()
                {
                    Id = 3,
                    Name = "iPhone 14 Leather Case with MagSafe - Ink",
                    Description = "A colorful family of accessories for easy attachment and faster wireless charging.",
                    ProductStatus = ProductStatus.OutOfStock,
                    NumberOfItems = 0
                }
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
            logger.LogInformation("{}: Completed seeding data to entity {}", nameof(SeedData), nameof(context.Products));
        }
        
        return context;
    }
}