using iot.Domain.Common;

namespace iot.Domain.Enums;

public class SensorType : Enumeration
{
    public static readonly SensorType Thermometer = new(1, nameof(Thermometer)); // 🌡️
    public static readonly SensorType Psychrometer = new(2, nameof(Psychrometer)); // 💦
    public static readonly SensorType Earthquake = new(3, nameof(Earthquake)); // ⚠️
    public static readonly SensorType SmellDetection = new (4, nameof(SmellDetection)); // 👃🏽

    public SensorType() { }

    public SensorType(int id, string name)
        : base(id, name) { }
}