using iot.Infrastructure.Common.Notifications.SmtpClientEmail;
using iot.Infrastructure.Persistence.Context.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace iot.Infrastructure;

public static class ConfigureService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ISmtpEmailService,SmtpEmailService>();

        return services;
    }

    public static IServiceCollection AddIdentityDbContextServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IIdentityContext, IdentityContext>();
        string cs = configuration.GetConnectionString("Development");
        services.AddDbContext<IdentityContext>(o =>
        {
            o.UseSqlServer(cs);
        });

        return services;
    }
}