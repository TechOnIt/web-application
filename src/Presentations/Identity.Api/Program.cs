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

// Infrastructure.
builder.Services.AddIdentityDbContextServices(builder.Configuration);
// Logics
builder.Services.AddApplicationServices();
builder.Services.AddMediatRServices();
//Register CommandeHandlers
builder.Services.AddMediatR(typeof(CommandHandler<,>).GetTypeInfo().Assembly);

//Register QueryHandlers
builder.Services.AddMediatR(typeof(QueryHandler<,>).GetTypeInfo().Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
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