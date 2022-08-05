using FluentValidation.AspNetCore;
using iot.Application.Commands;
using iot.Application.Commands.Roles.Management;
using iot.Application.Common.Behaviors;
using iot.Application.Common.Models;
using iot.Application.Queries;
using iot.Application.Repositories.SQL.LoginHistories;
using iot.Application.Repositories.SQL.Roles;
using iot.Application.Repositories.SQL.Users;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace iot.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<ILoginHistoryRepository, LoginHistoryRepository>()
            ;

        services
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidateCommandBehavior<,>))
            ;

        return services;
    }

    public static IServiceCollection AddMediatRServices(this IServiceCollection services)
    {
        //services.AddMediatR(Assembly.GetExecutingAssembly());
        //Register CommandeHandlers
        services.AddMediatR(typeof(CommandHandler<,>).GetTypeInfo().Assembly);

        //Register QueryHandlers
        services.AddMediatR(typeof(QueryHandler<,>).GetTypeInfo().Assembly);

        return services;
    }

    public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
    {
        // Automatic Validation.
        // https://github.com/FluentValidation/FluentValidation.AspNetCore#automatic-validation
        services.AddFluentValidationAutoValidation();

        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddFluentValidation(fv =>  // Fluent Validation
        {
            fv.RegisterValidatorsFromAssemblyContaining<BaseFluentValidator<object>>();
        });

        // Register validator's.
        services
            // Users.management
            .AddScoped<IValidator<CreateRoleCommand>, CreateRoleCommandValidator>()
            ;

        return services;
    }
}