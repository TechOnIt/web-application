using iot.Domain.Enums;
using System;

namespace iot.Domain.Entities.Product;

public class Sensor
{
    #region constructure
    public Sensor()
    {

    }

    public Sensor(Guid id,SensorType sensorType,Guid placeId)
    {
        Id = id;
        SensorType = sensorType;
        PlaceId = placeId;
    }
    #endregion

    public Guid Id { get; set; }
    public SensorType SensorType { get; private set; }

    #region methods
    public void SetSensorType(string typeName)
    {
        SensorType = SensorType.FromDisplayName<SensorType>(typeName);
    }

    public void SetSensorType(int value)
    {
        SensorType = SensorType.FromValue<SensorType>(value);
    }

    public SensorType GetSensorType()
        => SensorType;
    #endregion

    #region relations & foreignkeys
    public Guid PlaceId { get; set; }
    #endregion
}
