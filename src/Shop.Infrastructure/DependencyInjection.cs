using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Interfaces;
using Shop.Infrastructure.Persistence;

namespace Shop.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection UseShopInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connString = config.GetConnectionString("Db") ?? throw new NullReferenceException("ConnectionStrings:Db is null!");
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(o => o.UseNpgsql(connString));

        return services;
    }
}