using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace TechOnIt.Application.Services.AssemblyServices;

public class AppSettingsService<T> : IAppSettingsService<T> where T : class, new()
{
    #region constructor
    private readonly IWebHostEnvironment _environment;
    private readonly IOptionsMonitor<T> _options;
    private readonly IConfigurationRoot _configuration;
    private readonly string _section;
    private readonly string _file;

    public AppSettingsService(
        IWebHostEnvironment environment,
        IOptionsMonitor<T> options,
        IConfigurationRoot configuration,
        string section,
        string file)
    {
        _environment = environment;
        _options = options;
        _configuration = configuration;
        _section = section;
        _file = file;
    }
    #endregion

    public T Value => _options.CurrentValue;

    /// <summary>
    /// Update a specitif section in appsettings.json.
    /// </summary>
    /// <param name="applyChanges">TODO:Ashkan</param>
    public void Update(Action<T> applyChanges)
    {
        var fileProvider = _environment.ContentRootFileProvider;
        var fileInfo = fileProvider.GetFileInfo(_file);
        var physicalPath = fileInfo.PhysicalPath;

        // TODO:
        // Complete this.
        //var Object = JsonSerializer.Deserialize<Object>(File.ReadAllText(physicalPath));
        //var sectionObject = Object.TryGetValue(_section, out JToken section) ?
        //    JsonSerializer.Deserialize<T>(section.ToString()) : Value ?? new T();

        //applyChanges(sectionObject);

        //jObject[_section] = JObject.Parse(JsonSerializer.Serialize(sectionObject));
        //File.WriteAllText(physicalPath, JsonSerializer.Serialize(jObject, options: new JsonSerializerOptions
        //{
        //    WriteIndented = true
        //}));

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        _configuration.Reload();
    }
}
