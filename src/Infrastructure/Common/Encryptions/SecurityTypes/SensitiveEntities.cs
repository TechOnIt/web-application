using iot.Domain.Common;

namespace iot.iot.Infrastructure.Common.Encryptions.SecurityTypes;

public class SensitiveEntities : Enumeration
{
    public static readonly SensitiveEntities Device = new(2, nameof(Device));
    public static readonly SensitiveEntities Sensor = new(1, nameof(Sensor));
    public static readonly SensitiveEntities Reports = new(3, nameof(Reports));
    public static readonly SensitiveEntities Users = new(4, nameof(Users));

    public SensitiveEntities()
    {

    }

    public SensitiveEntities(int id, string name)
        : base(id, name)
    {
    }
}
