using FluentValidation.AspNetCore;
using iot.Application.Commands.Roles.Management;
using iot.Application.Common.Behaviors;
using iot.Application.Common.Exceptions;
using iot.Application.Repositories.SQL.LoginHistories;
using iot.Application.Repositories.SQL.Roles;
using iot.Application.Repositories.SQL.Users;
using iot.Infrastructure.Common.JwtBearerService;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Security.Claims;
using System.Text;

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

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidateCommandBehavior<,>));
        services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(CommitCommandBehavior<,>));

        return services;
    }

    public static IServiceCollection AddMediatRServices(this IServiceCollection services)
    {
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

    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
    {
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
}