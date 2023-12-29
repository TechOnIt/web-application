using TechOnIt.Domain.Common;

namespace TechOnIt.Domain.Enums;

public class RelayType : Enumeration
{
    public static readonly RelayType Light = new(1, nameof(Light)); // 💡
    public static readonly RelayType Heater = new(2, nameof(Heater)); // 🔥
    public static readonly RelayType Cooler = new(3, nameof(Cooler)); // ❄️
    public static readonly RelayType Fan = new(4, nameof(Fan)); // 💨

    public RelayType() { }

    public RelayType(int id, string name)
        : base(id, name) { }
}