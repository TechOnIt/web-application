using AspNetCoreRateLimit;
using iot.Application;
using iot.Application.Common.DTOs.Settings;
using iot.Infrastructure;
using iot.Infrastructure.Common.Extentions;
using NLog;
using NLog.Web;
using System.Reflection;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();
    builder.Services.AddHsts(opts =>
    {
        opts.MaxAge = TimeSpan.FromDays(365);
        opts.IncludeSubDomains = true;
        opts.Preload = true;
    });

    // https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.dataprotection.idataprotectionprovider?view=aspnetcore-6.0
    // builder.Services.AddDataProtection()
    //    .SetDefaultKeyLifetime(TimeSpan.FromDays(14));

    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Map app setting json to app setting object.
    // https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows
    builder.Services.Configure<AppSettingDto>(builder.Configuration.GetSection(nameof(AppSettingDto)));
    builder.Services.ConfigureWritable<AppSettingDto>(builder.Configuration.GetSection("AppSettingDto"));

    ConfigureServices(builder.Services);

    var app = builder.Build();

    // middlewares
    // if you want to catch all exceptions by custom middleware Uncomment the following line
    // And if you don't need it, then comment the following line
    app.UseCustomExceptionHandler();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        // client exception handle : https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-6.0#exception-handler-lambda
        app.UseHsts(); // https://git.ir/pluralsight-protecting-sensitive-data-from-exposure-in-asp-net-and-asp-net-core-applications/ episode 13
    }

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseIpRateLimiting();

    #region Custom Middleware
    // Initialize database data seed.
    await app.InitializeDatabaseAsync(builder);

    #endregion
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

        endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=General}/{action=Index}/{id?}");

        endpoints.MapControllers();
    });

    await app.RunAsync();
}
catch (Exception ex)
{
    // NLog: catch setup errors
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    LogManager.Shutdown();
}


void ConfigureServices(IServiceCollection services) // clean code 
{
    services.AddInfrastructureServices();
    services.AddApplicationServices();

    // Register CommandeHandlers
    services.AddMediatR(typeof(CommandHandler<,>).GetTypeInfo().Assembly);

    // Register QueryHandlers
    services.AddMediatR(typeof(QueryHandler<,>).GetTypeInfo().Assembly);

    services.AddFluentValidationServices();

    #region Api Limit-rate
    services.AddMemoryCache();
    services.Configure<IpRateLimitOptions>(options =>
    {
        options.EnableEndpointRateLimiting = true;
        options.StackBlockedRequests = false;
        options.HttpStatusCode = 429;
        options.RealIpHeader = "X-Real-IP";
        options.ClientIdHeader = "X-ClientId";
        options.GeneralRules = new List<RateLimitRule>
        {
            new RateLimitRule
            {
                Endpoint = "POST:/v1/Authentication/Signin",
                Period = "10s",
                Limit = 1,
            },
            new RateLimitRule
            {
                Endpoint = "POST:/v1/Authentication/SignOut",
                Period = "10s",
                Limit = 1,
            },
            new RateLimitRule
            {
                Endpoint = "POST:/v1/Authentication/SignUp",
                Period = "10s",
                Limit = 1,
            },
            new RateLimitRule
            {
                Endpoint = "POST:/v1/Manage/User/Create",
                Period = "10s",
                Limit = 1,
            },
             new RateLimitRule
            {
                Endpoint = "POST:/v1/Manage/User/Update",
                Period = "10s",
                Limit = 1,
            },
             new RateLimitRule
            {
                Endpoint = "POST:/v1/Manage/User/SetPassword",
                Period = "10s",
                Limit = 1,
            },
             new RateLimitRule
            {
                Endpoint = "POST:/v1/Manage/User/Ban",
                Period = "10s",
                Limit = 1,
            },
            new RateLimitRule
            {
                Endpoint = "POST:/v1/Manage/User/UnBan",
                Period = "10s",
                Limit = 1,
            },
             new RateLimitRule
            {
                Endpoint = "POST:/v1/Manage/User/RemoveAccount",
                Period = "10s",
                Limit = 1,
            },
             new RateLimitRule
            {
                Endpoint = "POST:/v1/Manage/User/ForceDelete",
                Period = "10s",
                Limit = 1,
            },
            new RateLimitRule
            {
                Endpoint = "Get:/v1/Manage/User/GetUsers",
                Period = "10s",
                Limit = 1,
            }
        };
    });
    services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
    services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
    services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
    services.AddInMemoryRateLimiting();
    #endregion
}

public static partial class Program { }