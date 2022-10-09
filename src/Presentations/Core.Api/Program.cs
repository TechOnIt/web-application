using iot.Application;
using iot.Application.Commands;
using iot.Application.Common.DTOs.Settings;
using iot.Application.Queries;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Infrastructure & Database.
builder.Services.AddIdentityDbContextServices(builder.Configuration);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

// Read json setting.
builder.Services.Configure<AppSettingDto>(builder.Configuration.GetSection(nameof(AppSettingDto)));
builder.Services.ConfigureWritable<AppSettingDto>(builder.Configuration.GetSection("AppSettingDto"));

//Register CommandeHandlers
builder.Services.AddMediatR(typeof(CommandHandler<,>).GetTypeInfo().Assembly);

//Register QueryHandlers
builder.Services.AddMediatR(typeof(QueryHandler<,>).GetTypeInfo().Assembly);

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