using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;
using TechOnIt.Application.Commands.Users.Authentication.SignInCommands;
using TechOnIt.Application.Common.DTOs.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Admin Api", Version = "v1" });
});

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

// Register MediatR.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SignInUserCommand).Assembly));

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

app.UseStaticFiles(new StaticFileOptions()
{
    HttpsCompression = Microsoft.AspNetCore.Http.Features.HttpsCompressionMode.Compress,
    OnPrepareResponse = (context) =>
    {
        var headers = context.Context.Response.GetTypedHeaders();
        headers.CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
        {
            Public = true,
            MaxAge = TimeSpan.FromDays(100)
        };
    }
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "Admin Api v1");
});
app.UseSwagger();

// Initialize database seed.
await app.InitializeDatabaseAsync(builder);

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
void ConfigureServices(IServiceCollection services)
{
    builder.Services.AddInfrastructureServices();
    builder.Services.AddApplicationServices();
    builder.Services.AddFluentValidationServices();
}