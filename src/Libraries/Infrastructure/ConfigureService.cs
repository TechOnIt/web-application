using Microsoft.Extensions.DependencyInjection;
using TechOnIt.Infrastructure.Caching;
using TechOnIt.Infrastructure.Caching.Contracts;
using TechOnIt.Infrastructure.Common.Helpers;
using TechOnIt.Infrastructure.Common.Notifications.KaveNegarSms;
using TechOnIt.Infrastructure.Common.Notifications.SmtpClientEmail;
using TechOnIt.Infrastructure.Persistence.Context;
using TechOnIt.Infrastructure.Persistence.SeedInitializer;
using TechOnIt.Infrastructure.Persistence.SeedInitializer.Identities;

namespace TechOnIt.Infrastructure;

public static class ConfigureService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IdentityContext>();

        services.AddTransient<ISmtpEmailService, SmtpEmailService>()
            .AddTransient<IKaveNegarSmsService, KaveNegarSmsService>();

        services.AddTransient<IDataInitializer, UserDataInitializer>();
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = ContextHelper.GetConnectionString("Redis");
            options.InstanceName = ContextHelper.GetSectionValue("Redis:InstanceName");
        });

        services.AddScoped<IFallbackCache, FallbackCache>();
        services.AddScoped<IRedisService, RedisService>();
        return services;
    }
}