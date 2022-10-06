using iot.Infrastructure.Common.Encryptions.Contracts;
using iot.Infrastructure.Common.Encryptions;
using iot.Infrastructure.Common.Notifications.KaveNegarSms;
using iot.Infrastructure.Common.Notifications.SmtpClientEmail;
using iot.Infrastructure.Persistence.Context.Identity;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace iot.Infrastructure;

public static class ConfigureService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IdentityContext>();
        services.AddTransient<ISmtpEmailService, SmtpEmailService>();
        services.AddTransient<IKaveNegarSmsService, KaveNegarSmsService>();
        services.AddTransient<IEncryptionHandlerService, EncryptionHandlerService>();

        //services.AddDataProtection()
        //    .PersistKeysToFileSystem(new DirectoryInfo(""))
        //    .ProtectKeysWithAzureKeyValult(
        //    "<KeyIdentifier>",
        //    "<ClientId>",
        //    "<clientSecret>"
        //    );

        return services;
    }
    public static IServiceCollection AddIdentityDbContextServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IdentityContext>();

        //string cs = configuration.GetConnectionString("Development");
        //if (cs != null)
        //    services.AddDbContext<IdentityContext>(o =>
        //    {
        //        o.UseSqlServer(cs);
        //    });

        // using Microsoft.AspNetCore.DataProtection;
        //services.AddDataProtection()
        //    .PersistKeysToDbContext<IdentityContext>()
        //    .SetDefaultKeyLifetime(TimeSpan.FromDays(14));

        return services;
    }
}