using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TechOnIt.Application.Common.Interfaces.Clients.Notifications;
using TechOnIt.Infrastructure.Caching;
using TechOnIt.Infrastructure.Caching.Contracts;
using TechOnIt.Infrastructure.Common.Helpers;
using TechOnIt.Infrastructure.Common.Notifications.KaveNegarSms;
using TechOnIt.Infrastructure.Common.Notifications.SmtpClientEmail;
using TechOnIt.Infrastructure.Persistence.SeedInitializer;
using TechOnIt.Infrastructure.Persistence.SeedInitializer.Identities;
using TechOnIt.Infrastructure.Repositories.UnitOfWorks;

namespace TechOnIt.Infrastructure;

public static class ConfigureService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWorks, UnitOfWork>();

        // SQL server config
        services.RegisterAndConfigSql(configuration);

        services.AddTransient<ISmtpEmailSender, SmtpEmailService>()
            .AddTransient<ISmsSender, KaveNegarSmsService>();

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

    /// <summary>
    /// Apply migrations on SQL server.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IHost ApplyMigrationToSQL(this IHost app)
    {
        try
        {
            // Automatically apply migrations at startup.
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return app;
    }

    private static IServiceCollection RegisterAndConfigSql(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        if (string.IsNullOrEmpty(connectionString))
        {
            // TODO: Log here! (connectionstring was not found.)
            return services;
        }
        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            options.UseSqlServer(connectionString, sqlOptions =>
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null))
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.None)));
        });

        services.AddScoped<IAppDbContext, AppDbContext>();

        return services;
    }
}