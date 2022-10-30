using iot.Infrastructure.Initializer;
using iot.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace iot.Infrastructure.Common.Extentions;

public static class ApplicationBuilderExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication? app, WebApplicationBuilder? builder)
    {
        // Handle null exception.
        if (app == null || builder == null)
            return;
        // Read RunDbInitializer boolean from app setting json.
        var beInitializedDb = bool.Parse(builder.Configuration.GetValue<string>("RunDbInitializer"));
        if (!beInitializedDb)
            return;

        await using var scope = app.Services
            .GetRequiredService<IServiceScopeFactory>()
            .CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetService<IdentityContext>(); //Service locator

        await dbContext!.Database.MigrateAsync();

        var dataInitializers = scope.ServiceProvider.GetServices<IDataInitializer>();
        foreach (var dataInitializer in dataInitializers)
            await dataInitializer.InitializeDataAsync();

        // TODO:
        // Log here.
        //Log.Information("database migrations started");

        // Update appSetting.json file.
        //await UpdateRunDbInitializerSettingsAsync();
    }

    private static async Task UpdateRunDbInitializerSettingsAsync()
    {
        // TODO:
        // Ashkan
        // Set false -> RunDbInitializer in appSetting.json file.
        var json = await File.ReadAllTextAsync("appsettings.json");
        dynamic jsonObj = JsonSerializer.Deserialize<object>(json)!;

        jsonObj["RunDbInitializer"] = false; // if no sectionpath just set the value

        string output = JsonSerializer.Serialize(jsonObj,
            options: new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync("appsettings.json", output);
    }
}