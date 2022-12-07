using System;
using TechOnIt.Domain.Entities.Product.StructureAggregate;
using TechOnIt.Domain.Enums;

namespace TechOnIt.Domain.Entities.Product;

public class Device
{
    #region constructure
    public Device(int pin, DeviceType deviceType, bool isHigh, Guid placeId)
    {
        Id = Guid.NewGuid();
        Pin = pin;
        DeviceType = deviceType;
        IsHigh = isHigh;
        PlaceId = placeId;
    }

    public Device(Guid id, int pin, DeviceType deviceType, bool isHigh, Guid placeId)
    {
        Id = id;
        Pin = pin;
        DeviceType = deviceType;
        IsHigh = isHigh;
        PlaceId = placeId;
    }

    public Device()
    {
    }
    #endregion

    private Guid? _Id;
    public Guid Id
    {
        get { return _Id ?? Guid.NewGuid(); }
        private set { _Id = value; }
    }
    public int Pin { get; set; }
    public DeviceType DeviceType { get; private set; }
    public bool IsHigh { get; set; }

    //Concurrency : from entityframework
    public byte[] RowVersion { get; set; }

    #region methods

    public Guid SetDeviceId(Guid id)
    {
        ArgumentNullException.ThrowIfNull(id);

        Id = id;
        return Id;
    }

    public Guid GetDeviceId()
    {
        return Id;
    }

    public void SetDeviceType(string typeName)
    {
        DeviceType = Common.Enumeration.FromDisplayName<DeviceType>(typeName);
    }

    public void SetDeviceType(int value)
    {
        DeviceType = Common.Enumeration.FromValue<DeviceType>(value);
    }

    public void SetDeviceType(DeviceType deviceType)
    {
        DeviceType = deviceType;
    }

    public DeviceType GetDeviceType()
        => DeviceType;
    #endregion

    #region relations & foreignkeys
    public Guid PlaceId { get; set; }
    public Place Place { get; set; }
    #endregion
}
