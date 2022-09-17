using Microsoft.EntityFrameworkCore;
using Shop.Application.Models;

namespace Shop.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().ToTable("Product");
    }
}