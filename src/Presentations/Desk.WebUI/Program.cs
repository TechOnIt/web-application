using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using System.Web.Helpers;
using TechOnIt.Application.Commands.Device.CreateDevice;
using TechOnIt.Application.Common.DTOs.Settings;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
// Authentication & Authorization
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
    options =>
    {
        options.AccessDeniedPath = "/forbidden";
        options.LoginPath = "/authentication/signin";
        builder.Configuration.Bind("CookieSettings", options);
    });
builder.Services.AddAuthorization();

RegisterMediatRCommands(builder.Services);

// Map app setting json to app setting object.
// https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows
builder.Services.Configure<AppSettingDto>(builder.Configuration.GetSection("SiteSettings"));
builder.Services.ConfigureWritable<AppSettingDto>(builder.Configuration.GetSection("SiteSettings"));

ConfigureServices(builder.Services);

// Add PWA service
builder.Services.AddProgressiveWebApp();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
// Routing
app.UseRouting();
// Auth
app.UseAuthentication();
app.UseAuthorization();
// Endpoints
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=General}/{action=Index}/{id?}");
app.Run();

void ConfigureServices(IServiceCollection services)
{
    builder.Services.AddInfrastructureServices();
    builder.Services.AddApplicationServices();
    builder.Services.AddFluentValidationServices();
}
void RegisterMediatRCommands(IServiceCollection services)
{
    services.AddMediatR(typeof(CreateDeviceCommand).GetTypeInfo().Assembly);
}