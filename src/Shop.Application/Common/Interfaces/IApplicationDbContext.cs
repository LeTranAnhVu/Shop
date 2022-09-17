using Microsoft.EntityFrameworkCore;
using Shop.Application.Features.ProductFeature.Models;
using Shop.Application.Models;

namespace Shop.Application.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Product> Products { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}