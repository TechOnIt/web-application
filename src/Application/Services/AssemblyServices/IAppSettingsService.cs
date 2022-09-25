namespace iot.Application.Services.AssemblyServices;

public interface IAppSettingsService<T> where T : class, new()
{
    T Value { get; }
    void Update(Action<T> applyChanges);
}