using iot.Application.Common.Extentions;

namespace iot.Domain.Enums;

public class StuctureType : Enumeration
{
    public static readonly StuctureType Farm = new(1, nameof(Farm));
    public static readonly StuctureType Garden = new(2, nameof(Garden));
    public static readonly StuctureType Aviculture = new(3, nameof(Aviculture));
    public static readonly StuctureType Agriculture = new(4, nameof(Aviculture));

    public StuctureType()
    {

    }

    public StuctureType(int id, string name)
        : base(id, name)
    {
    }
}
