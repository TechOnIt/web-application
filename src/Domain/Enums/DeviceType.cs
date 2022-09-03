using iot.Application.Common.Extentions;

namespace iot.Domain.Enums;

public class DeviceType : Enumeration
{
    public static readonly SensorType Light = new(1, nameof(Light)); // 💡
    public static readonly DeviceType Heater = new(2, nameof(Heater)); // 🔥
    public static readonly DeviceType Cooler = new (3, nameof(Cooler)); // ❄️
    public static readonly DeviceType Fan = new (4, nameof(Fan)); // 💨

    public DeviceType() { }

    public DeviceType(int id, string name)
        : base(id, name) { }
}