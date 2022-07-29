using FluentValidation.AspNetCore;
using iot.Application.Common.Behaviors;
using iot.Application.Common.Models;
using iot.Application.Repositories.SQL.Users;
using MediatR;
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

        services
            .AddScoped<IUserRepository, UserRepository>()
            ;

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidateCommandBehavior<,>));

        return services;
    }
}