using Microsoft.AspNetCore.Authentication.Cookies;
using TechOnIt.Application.Commands.Users.Authentication.SignInCommands;
using TechOnIt.Application.Common.DTOs.Settings;
using TechOnIt.Desk.WebUI.Hubs;
using TechOnIt.Desk.WebUI.RealTimeServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();


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

// Register MediatR.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SignInUserCommand).Assembly));

// Map app setting json to app setting object.
// https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows
builder.Services.Configure<AppSettingDto>(builder.Configuration.GetSection("SiteSettings"));
builder.Services.ConfigureWritable<AppSettingDto>(builder.Configuration.GetSection("SiteSettings"));
builder.Services.AddHostedService<TcpListenerHandler>();
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
    builder =>
    {
        builder.AllowAnyMethod().AllowAnyHeader()
               .WithOrigins("http://example.com") 
               .AllowCredentials();
    }));

ConfigureServices(builder.Services);

builder.Services.AddResponseCaching();

// Add PWA service
builder.Services.AddProgressiveWebApp();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Configure the HTTP request pipeline.
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

// Routing
app.UseRouting();
app.UseResponseCaching();
// Auth
app.UseAuthentication();
app.UseAuthorization();

// Endpoints
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Overview}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<SensorHub>("/SensorHub");

app.Run();

void ConfigureServices(IServiceCollection services)
{
    builder.Services.AddInfrastructureServices();
    builder.Services.AddApplicationServices();
    builder.Services.AddFluentValidationServices();
}