using System.ComponentModel;

namespace TechOnIt.Application.Services.Settings;

public class SettingService : ISettingService
{
    public void SaveSetting<T>(T settings) where T : ISettings, new()
    {
        Dictionary<string, string> settingKeyValuePair = new();
        /* We do not clear cache after each setting update.
         * This behavior can increase performance because cached settings will not be cleared 
         * and loaded from database after each update */
        foreach (var prop in typeof(T).GetProperties())
        {
            // get properties we can read and write to
            if (!prop.CanRead || !prop.CanWrite)
                continue;
            if (!TypeDescriptor.GetConverter(prop.PropertyType)
                .CanConvertFrom(typeof(string)))
                continue;

            var key = typeof(T).Name + "." + prop.Name;
            var value = prop.GetValue(settings, null);
            string valueString = (value != null ?
                TypeDescriptor.GetConverter(prop.PropertyType)
                .ConvertToInvariantString(value) :
                string.Empty);

            settingKeyValuePair.Add(key, valueString);
        }
    }
}