using FluentValidation.AspNetCore;
using iot.Application.Common.Models;
using Microsoft.Extensions.DependencyInjection;

namespace iot.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddFluentValidation(fv =>  // Fluent Validation
        {
                fv.RegisterValidatorsFromAssemblyContaining<BaseFluentValidator<object>>();
        });

        return services;
    }
}