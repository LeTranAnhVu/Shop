using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Common.Behaviors;

namespace Shop.Application;

public static class DependencyInjection
{
    public static IServiceCollection UseShopApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(new List<Assembly>{Assembly.GetExecutingAssembly()});
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}