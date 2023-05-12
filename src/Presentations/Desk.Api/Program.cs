using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.Types;
using System.Text.Json.Serialization;
using TechOnIt.Application;
using TechOnIt.Application.Commands.Users.Authentication.SignInCommands;
using TechOnIt.Application.Common.DTOs.Settings;
using TechOnIt.Desk.Api.GraphQl.SensorReport;
using TechOnIt.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register MediatR.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SignInUserCommand).Assembly));

// Map app setting json to app setting object.
// https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows
builder.Services.Configure<AppSettingDto>(builder.Configuration.GetSection("SiteSettings"));
builder.Services.ConfigureWritable<AppSettingDto>(builder.Configuration.GetSection("SiteSettings"));

ConfigureServices(builder.Services);
var app = builder.Build();

// middlewares
// if you want to catch all exceptions by custom middleware Uncomment the following line
// And if you don't need it, then comment the following line
app.UseApiExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // add altair UI to development only
    app.UseGraphQLAltair();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors();
//app.UseAuthorization();
// make sure all our schemas registered to route
app.UseGraphQL<ISchema>();

#region Endpoint
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Swagger}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=General}/{action=Index}/{id?}");
app.MapControllers();
#endregion

await app.RunAsync();

void ConfigureServices(IServiceCollection services)
{
    var appSetting = builder.Configuration.GetSection("SiteSettings").Get<AppSettingDto>();

    services.AddInfrastructureServices()
        .AddApplicationServices()
        .AddFluentValidationServices()
        .AddJwtAuthentication(appSetting.JwtSettings);

    // Add services to the container.
    // add notes schema
    builder.Services.AddSingleton<ISchema, ReportSchema>(services => new ReportSchema(new SelfActivatingServiceProvider(services)));
    // register graphQL
    builder.Services.AddGraphQL(options =>
    {
        options.EnableMetrics = true;
    }).AddSystemTextJson();

    services.AddCors(options =>
    {
        options.AddDefaultPolicy(
            builder =>
            {
                builder.WithOrigins("*")
                       .AllowAnyHeader();
            });
    });
}