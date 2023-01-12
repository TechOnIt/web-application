namespace TechOnIt.Infrastructure.Persistence.SeedInitializer;

internal interface IDataInitializer
{
    Task InitializeDataAsync();
}