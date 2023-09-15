namespace TechOnIt.Application.Services.Settings
{
    /// <summary>
    /// Setting service interface
    /// </summary>
    public partial interface ISettingService
    {
        void SaveSetting<T>(T settings) where T : ISettings, new();
    }
}