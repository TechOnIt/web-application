using iot.Infrastructure.Common.Notifications.KaveNegarSms;
using iot.Infrastructure.Common.Notifications.SmtpClientEmail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace iot.Infrastructure;

public static class ConfigureService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ISmtpEmailService,SmtpEmailService>();
        services.AddTransient<IKaveNegarSmsService, KaveNegarSmsService>();

        return services;
    }
}