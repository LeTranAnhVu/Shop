using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shop.Application.Common.Interfaces;
using Shop.Application.Features.ProductFeature.Models;
using Shop.Application.Models.Enums;
using Shop.Infrastructure.Persistence;
using Shop.Infrastructure.SeedData;

namespace Shop.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddShopInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connString = config.GetConnectionString("Db") ?? throw new NullReferenceException("ConnectionStrings:Db is null!");
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(o => o.UseNpgsql(connString));

        return services;
    }
    
    public static async Task SeedData(this IApplicationBuilder app)
    {
        var logger = app.ApplicationServices.GetRequiredService<ILogger<IApplicationDbContext>>();
        var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        await context.SeedProductsAsync(logger);
    }
}