using iot.Application.Common.Extentions;

namespace iot.Domain.Enums;

public class StuctureType : Enumeration
{
    public static readonly StuctureType Home = new(1, nameof(Home));
    public static readonly StuctureType Agriculture = new(2, nameof(Aviculture));
    public static readonly StuctureType Aviculture = new(3, nameof(Aviculture));

    public StuctureType()
    {

    }

    public StuctureType(int id, string name)
        : base(id, name)
    {
    }
}