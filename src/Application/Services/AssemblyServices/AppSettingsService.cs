using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace iot.Application.Services.AssemblyServices;

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

    public void Update(Action<T> applyChanges)
    {
        var fileProvider = _environment.ContentRootFileProvider;
        var fileInfo = fileProvider.GetFileInfo(_file);
        var physicalPath = fileInfo.PhysicalPath;

        var jObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(physicalPath));
        var sectionObject = jObject.TryGetValue(_section, out JToken section) ?
            JsonConvert.DeserializeObject<T>(section.ToString()) : (Value ?? new T());

        applyChanges(sectionObject);

        jObject[_section] = JObject.Parse(JsonConvert.SerializeObject(sectionObject));
        File.WriteAllText(physicalPath, JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented));

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        _configuration.Reload();
    }
}
