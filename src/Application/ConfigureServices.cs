using FluentValidation.AspNetCore;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TechOnIt.Application.Commands.Roles.Management.CreateRole;
using TechOnIt.Application.Common.Behaviors;
using TechOnIt.Application.Common.DTOs.Settings;
using TechOnIt.Application.Common.Frameworks.Middlewares;
using TechOnIt.Application.Common.Security.JwtBearer;
using TechOnIt.Application.Reports.Devices;
using TechOnIt.Application.Reports.Roles;
using TechOnIt.Application.Reports.StructuresAggregate;
using TechOnIt.Application.Reports.Users;
using TechOnIt.Application.Services.AssemblyServices;
using TechOnIt.Application.Services.Authenticateion;
using TechOnIt.Application.Services.Authenticateion.AuthenticateionContracts;
using TechOnIt.Application.Services.Authenticateion.StructuresService;

namespace TechOnIt.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services
            .AddScoped<IUnitOfWorks, UnitOfWork>();

        services
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidateCommandBehavior<,>))
            ;

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidateCommandBehavior<,>));
        services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(CommitCommandBehavior<,>));

        // Add cache service.
        services.AddDistributedMemoryCache();
        services.AddReportServices();
        services.AuthenticationCustomServices();

        return services;
    }

    public static void ConfigureWritable<T>(
            this IServiceCollection services,
            IConfigurationSection section,
            string file = "appsettings.json") where T : class, new()
    {
        services.AddTransient<IAppSettingsService<T>>(provider =>
        {
            var configuration = (IConfigurationRoot)provider.GetService<IConfiguration>();
            var environment = provider.GetService<IWebHostEnvironment>();
            var options = provider.GetService<IOptionsMonitor<T>>();
            return new AppSettingsService<T>(environment, options, configuration, section.Key, file);
        });
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

    public static IServiceCollection AuthenticationCustomServices(this IServiceCollection services)
    {
        services.TryAddTransient<IIdentityService, IdentityService>();
        services.TryAddTransient<IStructureService, StructureService>();
        services.TryAddTransient<IRoleService, RoleService>();
        services.TryAddTransient<IUserService, UserService>();
        services.TryAddScoped<IJwtService, JwtService>();

        return services;
    }

    public static IServiceCollection AddReportServices(this IServiceCollection services)
    {
        services.AddScoped<IUserReports, UserReports>()
            .AddScoped<IDeviceReport, DeviceReport>()
            .AddScoped<IRoleReports, RoleReports>()
            .AddScoped<IStructureAggregateReports, StructureAggregateReports>();

        return services;
    }

    public static void UseCustomExceptionHandler(this WebApplication app)
    {
        app.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }

    public static void AddJwtAuthentication(this IServiceCollection services, JwtSettingsDto settings)
    {
        if (settings is null)
            return;
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(option =>
        {
            var secretKey = Encoding.UTF8.GetBytes(settings.SecretKey);
            var validationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                RequireSignedTokens = true,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ValidateAudience = false,
                ValidAudience = settings.Audience,

                ValidateIssuer = true,
                ValidIssuer = settings.Issuer,
            };

            option.RequireHttpsMetadata = false;
            option.SaveToken = true;
            option.TokenValidationParameters = validationParameters;
        });
    }
}