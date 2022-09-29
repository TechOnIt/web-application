using iot.Infrastructure.Common.Notifications.KaveNegarSms;
using iot.Infrastructure.Common.Notifications.SmtpClientEmail;
using iot.Infrastructure.Persistence.Context.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace iot.Infrastructure;

public static class ConfigureService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IdentityContext>();
        services.AddTransient<ISmtpEmailService,SmtpEmailService>();
        services.AddTransient<IKaveNegarSmsService, KaveNegarSmsService>();

        return services;
    }
}