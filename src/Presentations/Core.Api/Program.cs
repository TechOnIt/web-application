using NLog;
using NLog.Web;
using System.Text.Json.Serialization;
using TechOnIt.Application.Commands.Users.Authentication.SignInCommands;
using TechOnIt.Application.Common.DTOs.Settings;
using TechOnIt.Infrastructure;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

var builder = WebApplication.CreateBuilder(args);

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
ConfigureServices(builder.Services, builder.Configuration.GetSection("SiteSettings").Get<AppSettingDto>());
builder.Services.AddAuthorization();
// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAllowSpecificOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("*", "http://localhost:3000")
            .SetIsOriginAllowed(_ => true)
            .AllowCredentials()
            .WithMethods("GET", "PUT", "DELETE", "POST", "PATCH")
            ;
        });
});

var app = builder.Build();

// middlewares
// if you want to catch all exceptions by custom middleware Uncomment the following line
// And if you don't need it, then comment the following line
app.UseApiExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Routing
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Swagger}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=General}/{action=Index}/{id?}");

    endpoints.MapControllers();
});

await app.RunAsync();

void ConfigureServices(IServiceCollection services, AppSettingDto settings) // clean code 
{
    var jwtSetting = settings.JwtSettings;

    services.AddInfrastructureServices()
        .AddApplicationServices()
        .AddFluentValidationServices()
        .AddJwtAuthentication(jwtSetting);
}