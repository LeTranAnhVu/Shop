using Microsoft.EntityFrameworkCore;
using Shop.Application.Features.ProductFeature.Models;

namespace Shop.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Product> Products { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}