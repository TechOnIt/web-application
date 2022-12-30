using FluentValidation.AspNetCore;
using TechOnIt.Infrastructure.Common.JwtBearerService;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Security.Claims;
using System.Text;
using TechOnIt.Application.Commands.Roles.Management.CreateRole;
using TechOnIt.Application.Common.Behaviors;
using TechOnIt.Application.Common.Frameworks.Middlewares;
using TechOnIt.Application.Reports.Roles;
using TechOnIt.Application.Reports.StructuresAggregate;
using TechOnIt.Application.Reports.Users;
using TechOnIt.Application.Services.AssemblyServices;
using TechOnIt.Application.Services.Authenticateion;
using TechOnIt.Application.Services.Authenticateion.AuthenticateionContracts;
using TechOnIt.Application.Common.Security.JwtBearer;

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
        services.TryAddTransient<IRoleService, RoleService>();
        services.TryAddTransient<IUserService, UserService>();
        services.TryAddTransient<IJwtService, JwtService>();

        return services;
    }

    public static IServiceCollection AddCustomAuthenticationServices(this IServiceCollection services,JwtSettings settingsJwt)
    {
        var jwtSettings = new JwtSettings();
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var secretkeyHandler = settingsJwt.SecretKeyP == null ? JwtSettings.SecretKey : settingsJwt.SecretKeyP;
                var encryptionkeyHandler = settingsJwt.EncrypKeyP == null ? JwtSettings.EncrypKey : settingsJwt.EncrypKeyP;
                var ValidAudienceHandler = settingsJwt.AudienceP == null ? JwtSettings.Audience : settingsJwt.AudienceP;
                var ValidIssuerHandler = settingsJwt.IssuerP == null ? JwtSettings.Issuer : settingsJwt.IssuerP;


                var secretkey = Encoding.UTF8.GetBytes(secretkeyHandler);
                var encryptionkey = Encoding.UTF8.GetBytes(encryptionkeyHandler);

                var validationParameters = new TokenValidationParameters
                {
                    RequireSignedTokens = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretkey),

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ValidateAudience = true, //default : false
                    ValidAudience = ValidAudienceHandler,

                    ValidateIssuer = true, //default : false
                    ValidIssuer = ValidIssuerHandler,

                    TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey)
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = validationParameters;
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception != null)
                            throw new AppException(ApiResultStatusCode.UnAuthorized, "Authentication failed.", HttpStatusCode.Unauthorized, context.Exception, null);
                        return Task.CompletedTask;
                    },

                    OnTokenValidated = async context =>
                    {
                        var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                        if (claimsIdentity.Claims?.Any() != true)
                            context.Fail("This token has no claims.");

                        var securityStamp = claimsIdentity.FindFirst(new ClaimsIdentityOptions().SecurityStampClaimType);
                        if (securityStamp == null)
                            context.Fail("This token has no secuirty stamp");

                        var userId = claimsIdentity.FindFirst("UserId");
                    },

                    OnChallenge = context =>
                    {
                        if (context.AuthenticateFailure != null)
                            throw new AppException(ApiResultStatusCode.UnAuthorized, "Authenticate failure.", HttpStatusCode.Unauthorized, context.AuthenticateFailure, null);
                        throw new AppException(ApiResultStatusCode.UnAuthorized, "You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);
                    }

                };
            });

        return services;
    }

    public static IServiceCollection AddReportServices(this IServiceCollection services)
    {
        services.AddTransient<IUserReports, UserReports>();
        services.AddTransient<IRoleReports, RoleReports>();
        services.AddTransient<IStructureAggregateReports, StructureAggregateReports>();

        return services;
    }

    public static void UseCustomExceptionHandler(this WebApplication app)
    {
        app.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}