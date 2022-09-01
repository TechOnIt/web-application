using iot.Application.Common.Extentions;

namespace iot.Domain.Enums;

public class DeviceType : Enumeration
{
    public static readonly StuctureType Thermometer = new(1, nameof(Thermometer));
    public static readonly StuctureType HumidityMeter = new (2, nameof(HumidityMeter));
    public static readonly StuctureType Lamp = new(3, nameof(Lamp));
    public static readonly StuctureType CoolingFan = new (4, nameof(CoolingFan));

    public DeviceType()
    {

    }

    public DeviceType(int id, string name)
        : base(id, name)
    {
    }
}


// a sample for learn how you can use that Enummeration abstract class in childs

//public abstract class EmployeeType : Enumeration
//{
//    public static readonly EmployeeType Manager
//        = new ManagerType();

//    protected EmployeeType() { }
//    protected EmployeeType(int value, string displayName) : base(value, displayName) { }

//    public abstract decimal BonusSize { get; }

//    private class ManagerType : EmployeeType
//    {
//        public ManagerType() : base(0, "Manager") { }

//        public override decimal BonusSize
//        {
//            get { return 1000m; }
//        }
//    }
//}