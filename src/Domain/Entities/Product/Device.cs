using iot.Domain.Enums;
using System;

namespace iot.Domain.Entities.Product;

public class Device
{
    #region constructure
    public Device(Guid id,int pin, DeviceType deviceType, bool isHigh, Guid placeId)
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

    public Guid Id { get; set; }
    public int Pin { get; set; }
    public DeviceType DeviceType { get; private set; }
    public bool IsHigh { get; set; }

    #region methods

    public void SetDeviceType(string typeName)
    {
        DeviceType = DeviceType.FromDisplayName<DeviceType>(typeName);
    }

    public void SetDeviceType(int value)
    {
        DeviceType = DeviceType.FromValue<DeviceType>(value);
    }

    public DeviceType GetDeviceType()
        => this.DeviceType;
    #endregion

    #region relations & foreignkeys
    public Guid PlaceId { get; set; }
    #endregion
}
