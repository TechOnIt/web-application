using iot.Domain.Enums;
using System;
using System.Collections.Generic;

namespace iot.Domain.Entities.Product.SensorAggregate;

public class Sensor
{
    #region constructure
    public Sensor()
    {

    }

    public Sensor(Guid id, SensorType sensorType, Guid placeId)
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
        SensorType = Common.Enumeration.FromDisplayName<SensorType>(typeName);
    }

    public void SetSensorType(int value)
    {
        SensorType = Common.Enumeration.FromValue<SensorType>(value);
    }

    public void SetSensorType(SensorType sensorType)
    {
        SensorType = sensorType;
    }

    public SensorType GetSensorType()
        => SensorType;
    #endregion

    #region aggregate methods
    public void ClearReports()
    {
        this.Reports.Clear();
    }

    public void AddReport(PerformanceReport newReport)
    {
        this.Reports.Add(newReport);
    }

    public void RemoveReport(PerformanceReport newReport)
    {
        this.Reports.Remove(newReport);
    }

    public ICollection<PerformanceReport> GetReports()
        => this.Reports;
    #endregion

    #region relations & foreignkeys
    public Guid PlaceId { get; set; }

    public virtual ICollection<PerformanceReport> Reports { get; set; }
    #endregion
}
