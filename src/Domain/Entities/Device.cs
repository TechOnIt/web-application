using System;
using TechOnIt.Domain.Entities.StructureAggregate;

namespace TechOnIt.Domain.Entities;

public class Device
{
    #region constructure
    private Device() { }

    public Device(int pin, DeviceType type, Guid placeId)
    {
        Id = Guid.NewGuid();
        Pin = pin;
        Type = type;
        PlaceId = placeId;
    }
    #endregion

    public Guid Id { get; private set; }
    public int Pin { get; private set; }
    public DeviceType Type { get; private set; } = DeviceType.Light;
    public bool IsHigh { get; private set; } = false;

    //Concurrency : from entityframework
    public byte[]? RowVersion { get; private set; }

    #region methods
    public void SetDeviceType(DeviceType type)
    {
        Type = type;
    }

    public void High()
    {
        IsHigh = true;
    }

    public void Low()
    {
        IsHigh = false;
    }
    #endregion

    #region relations & foreignkeys
    public Guid PlaceId { get; private set; }
    public virtual Place? Place { get; private set; } = null;
    #endregion
}
