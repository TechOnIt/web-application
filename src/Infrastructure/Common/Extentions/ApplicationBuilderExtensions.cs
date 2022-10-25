using iot.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NLog.Fluent;

namespace iot.Infrastructure.Common.Extentions;

public static class ApplicationBuilderExtensions
{
    public static async Task InitializeDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var dbContext = scope.ServiceProvider.GetService<IdentityContext>(); //Service locator
        // TODO:
        //Log.Information("database migrations started");
    }
}