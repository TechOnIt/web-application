using AspNetCoreRateLimit;
using iot.Application;
using iot.Application.Commands;
using iot.Application.Queries;
using iot.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigureServices(builder.Services);

//builder.Services.AddCustomAuthenticationServices(builder.Configuration, );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseIpRateLimiting();

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

void ConfigureServices(IServiceCollection services) // clean code 
{
    // Infrastructure & Database.
    services.AddIdentityDbContextServices(builder.Configuration);
    // Logics

    services.AddApplicationServices();
    services.AddMediatRServices();

    //Register CommandeHandlers
    services.AddMediatR(typeof(CommandHandler<,>).GetTypeInfo().Assembly);

    //Register QueryHandlers
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