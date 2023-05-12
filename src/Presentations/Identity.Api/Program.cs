using AspNetCoreRateLimit;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System.Text.Json.Serialization;
using TechOnIt.Application.Commands.Users.Authentication.SignInCommands;
using TechOnIt.Application.Common.DTOs.Settings;
using TechOnIt.Infrastructure;
using TechOnIt.Infrastructure.Common.Extentions;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
    builder.Services.AddHsts(opts =>
    {
        opts.MaxAge = TimeSpan.FromDays(365);
        opts.IncludeSubDomains = true;
        opts.Preload = true;
    });

    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

    #region Logging
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    #endregion

    // Register MediatR.
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SignInUserCommand).Assembly));

    // Map app setting json to app setting object.
    // https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows
    builder.Services.Configure<AppSettingDto>(builder.Configuration.GetSection("SiteSettings"));
    builder.Services.ConfigureWritable<AppSettingDto>(builder.Configuration.GetSection("SiteSettings"));

    // Cross origin
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("allows_policy", policy =>
        {
            policy.AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("*", "http://localhost:3000")
                .SetIsOriginAllowed(_ => true)
                .AllowCredentials()
                .WithMethods("GET", "PUT", "DELETE", "POST", "PATCH"); //not really necessary when AllowAnyMethods is used.;
        });
    });
    builder.Services.AddRouting(options => options.LowercaseUrls = true);
    ConfigureServices(builder);
    builder.Services.AddAuthorization();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    #region swagger authorization
    var securityScheme = new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JSON Web Token based security",
    };
    var securityReq = new OpenApiSecurityRequirement()
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] {}
    }
};
    var contact = new OpenApiContact()
    {
        Name = "Tech On It",
        Email = "info@techonit.com",
        Url = new Uri("https://ashkannoori.onrender.com")
    };
    var license = new OpenApiLicense()
    {
        Name = "Free License",
        Url = new Uri("https://ashkannoori.onrender.com")
    };
    var info = new OpenApiInfo()
    {
        Version = "v1",
        Title = "TechOnIt SDK",
        Description = "Implementing JWT Authentication in SDK",
        TermsOfService = new Uri("https://ashkannoori.onrender.com"),
        Contact = contact,
        License = license
    };

    builder.Services.AddSwaggerGen(o =>
    {
        o.SwaggerDoc("v1", info);
        o.AddSecurityDefinition("Bearer", securityScheme);
        o.AddSecurityRequirement(securityReq);
    });

    #endregion

    var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI();

    #region HSTS
    if (!app.Environment.IsDevelopment())
    {
        // Configure the HTTP request pipeline.
        // client exception handle : https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-6.0#exception-handler-lambda
        app.UseHsts(); // https://git.ir/pluralsight-protecting-sensitive-data-from-exposure-in-asp-net-and-asp-net-core-applications/ episode 13

    }
    #endregion

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();

    #region Endpoint
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Swagger}/{action=Index}/{id?}");
    app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=General}/{action=Index}/{id?}");
    app.MapControllers();
    #endregion

    app.UseIpRateLimiting();

    #region Custom Middlewares
    // Initialize database data seed.
    await app.InitializeDatabaseAsync(builder);
    #region Exception Handlers
    // if you want to catch all exceptions by custom middleware Uncomment the following line
    // And if you don't need it, then comment the following line
    app.UseApiExceptionHandler();
    #endregion
    #endregion
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


void ConfigureServices(WebApplicationBuilder builder) // clean code 
{
    var jwtSetting = builder.Configuration.GetSection("SiteSettings").Get<AppSettingDto>().JwtSettings;

    builder.Services.AddInfrastructureServices()
        .AddApplicationServices()
        .AddFluentValidationServices()
        .AddJwtAuthentication(jwtSetting);

    #region Api Limit-rate
    builder.Services.AddMemoryCache();
    builder.Services.Configure<IpRateLimitOptions>(options =>
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
    builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
    builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
    builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
    builder.Services.AddInMemoryRateLimiting();
    #endregion
}

public static partial class Program { }