using iot.Infrastructure.Common.Encryptions;
using iot.Infrastructure.Common.Encryptions.Contracts;
using iot.Infrastructure.Common.Notifications.KaveNegarSms;
using iot.Infrastructure.Common.Notifications.SmtpClientEmail;
using iot.Infrastructure.Persistence.Context.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace iot.Infrastructure;

public static class ConfigureService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IdentityContext>();

        services.AddTransient<ISmtpEmailService, SmtpEmailService>()
            .AddTransient<IKaveNegarSmsService, KaveNegarSmsService>();

        services.AddTransient<IEncryptionHandlerService, EncryptionHandlerService>();

        //services.AddDataProtection()
        //    .PersistKeysToFileSystem(new DirectoryInfo(""))
        //    .ProtectKeysWithAzureKeyValult(
        //    "<KeyIdentifier>",
        //    "<ClientId>",
        //    "<clientSecret>"
        //    );

        // using Microsoft.AspNetCore.DataProtection;
        //services.AddDataProtection()
        //    .PersistKeysToDbContext<IdentityContext>()
        //    .SetDefaultKeyLifetime(TimeSpan.FromDays(14));

        return services;
    }
}