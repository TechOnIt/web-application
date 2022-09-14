using GraphQL.Instrumentation;
using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.Types;
using iot.Desk.Api.GraphQl.PerformanceReport;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigureServices(builder.Services);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // add altair UI to development only
    app.UseGraphQLAltair();
}

app.UseHttpsRedirection();

app.UseCors();
app.UseAuthorization();
// make sure all our schemas registered to route
app.UseGraphQL<ISchema>();

await app.RunAsync();


void ConfigureServices(IServiceCollection services)
{
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
