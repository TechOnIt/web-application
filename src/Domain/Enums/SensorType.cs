using iot.Application.Common.Extentions;

namespace iot.Domain.Enums;

public class SensorType : Enumeration
{
    public static readonly StuctureType Temperature = new(1, nameof(Temperature));
    public static readonly StuctureType Earthquake = new(2, nameof(Earthquake));
    public static readonly StuctureType Light = new(3, nameof(Light));
    public static readonly StuctureType EnergyConsumption = new (4, nameof(EnergyConsumption));

    public SensorType()
    {

    }

    public SensorType(int id, string name)
        : base(id, name)
    {
    }
}
