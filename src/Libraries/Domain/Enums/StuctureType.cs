using TechOnIt.Domain.Common;

namespace TechOnIt.Domain.Enums;

public class StructureType : Enumeration
{
    public static readonly StructureType Home = new(1, nameof(Home)); // 🏡
    public static readonly StructureType Agriculture = new(2, nameof(Aviculture)); // 🌱
    public static readonly StructureType Aviculture = new(3, nameof(Aviculture)); // 🐔

    public StructureType()
    {

    }

    public StructureType(int id, string name)
        : base(id, name)
    {
    }
}