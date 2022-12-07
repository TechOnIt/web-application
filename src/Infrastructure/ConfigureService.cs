using Microsoft.Extensions.DependencyInjection;
using TechOnIt.Infrastructure.Common.Encryptions;
using TechOnIt.Infrastructure.Common.Encryptions.Contracts;
using TechOnIt.Infrastructure.Common.Notifications.KaveNegarSms;
using TechOnIt.Infrastructure.Common.Notifications.SmtpClientEmail;
using TechOnIt.Infrastructure.Initializer;
using TechOnIt.Infrastructure.Initializer.Identities;
using TechOnIt.Infrastructure.Persistence.Context;

namespace TechOnIt.Infrastructure;

public static class ConfigureService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IdentityContext>();

        services.AddTransient<ISmtpEmailService, SmtpEmailService>()
            .AddTransient<IKaveNegarSmsService, KaveNegarSmsService>();

        services.AddTransient<IEncryptionHandlerService, EncryptionHandlerService>();

        // Database initializer.
        services.AddTransient<IDataInitializer, UserDataInitializer>();

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