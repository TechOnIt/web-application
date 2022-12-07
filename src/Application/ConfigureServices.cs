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
using TechOnIt.Application.Common.Frameworks.ApiResultFrameWork;
using TechOnIt.Application.Common.Frameworks.Middlewares;
using TechOnIt.Application.Common.Models;
using TechOnIt.Application.Reports.Roles;
using TechOnIt.Application.Reports.StructuresAggregate;
using TechOnIt.Application.Reports.Users;
using TechOnIt.Application.Services.AssemblyServices;
using TechOnIt.Application.Services.Authenticateion;
using TechOnIt.Application.Services.Authenticateion.AuthenticateionContracts;
using TechOnIt.Application.Services.ProductServices;
using TechOnIt.Application.Services.ProductServices.ProductContracts;

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

        services.AddCustomAuthenticationServices();
        services.AddReportServices();
        services.AuthenticationCustomServices();

        services.TryAddTransient<IDeviceService, DeviceService>();
        //services.ConfigureWritable<SiteSettings>(Configuration.GetSection("SiteSettings"));
        //services.TryAddTransient(typeof(IAppSettingsService<>), typeof(AppSettingsService<>));

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

        return services;
    }

    public static IServiceCollection AddCustomAuthenticationServices(this IServiceCollection services)
    {
        // TODO:
        // Refactor
        var jwtSettings = new JwtSettings();

        services
            .AddAuthentication(options =>
            {

            })
            .AddJwtBearer(options =>
            {
                JwtSettings settingsJwt = new JwtSettings();
                if (jwtSettings != null) settingsJwt = jwtSettings;

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

                        //var userId = claimsIdentity.GetUserId<string>();
                        var userId = claimsIdentity.FindFirst("UserId");
                        //var user = GetUserAsync(context.Principal);

                        //if (user.SecurityStamp != securityStamp)
                        //context.Fail("Token secuirty stamp is not valid.");

                        //    if (!user.IsActive)
                        //        context.Fail("User is not active.");
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