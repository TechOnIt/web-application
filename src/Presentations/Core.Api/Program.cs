using iot.Application.Common.DTOs.Settings;
using iot.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

// Read json setting.
builder.Services.Configure<AppSettingDto>(builder.Configuration.GetSection(nameof(AppSettingDto)));
builder.Services.ConfigureWritable<AppSettingDto>(builder.Configuration.GetSection("AppSettingDto"));

builder.Services.AddFluentValidationServices();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Routing
app.UseRouting();
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