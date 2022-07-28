using FluentValidation.AspNetCore;
using iot.Application.Models;
using Microsoft.Extensions.DependencyInjection;

namespace iot.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services
            // Fluent Validation
            .AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<BaseFluentValidator<object>>();
            })
            ;
        return services;
    }
}