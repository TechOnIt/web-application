using Microsoft.Extensions.Configuration;

namespace TechOnIt.Infrastructure.Common.Helpers;
public static class ContextHelper
{
    public static string GetConnectionString(string connectionStringName = "Database",string settingFileName = "appsettings.json")
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(settingFileName)
            .Build();

        return configuration.GetConnectionString(connectionStringName);
    }
    public static string GetSectionValue(string sectionName = "Database", string settingFileName = "appsettings.json")
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(settingFileName)
            .Build();

        return configuration.GetSection(sectionName).Value;
    }
}
