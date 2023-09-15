using Microsoft.Extensions.DependencyInjection;
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

        // Database initializer.
        services.AddTransient<IDataInitializer, UserDataInitializer>();

        return services;
    }
}