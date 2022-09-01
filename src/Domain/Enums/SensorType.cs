using iot.Application.Common.Extentions;

namespace iot.Domain.Enums;

public class SensorType : Enumeration
{
    public static readonly SensorType Temperature = new(1, nameof(Temperature));
    public static readonly SensorType Earthquake = new(2, nameof(Earthquake));
    public static readonly SensorType Light = new(3, nameof(Light));
    public static readonly SensorType EnergyConsumption = new (4, nameof(EnergyConsumption));

    public SensorType()
    {

    }

    public SensorType(int id, string name)
        : base(id, name)
    {
    }
}
