using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Shop.Application;

public static class DependencyInjection
{
    public static IServiceCollection UseShopApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}