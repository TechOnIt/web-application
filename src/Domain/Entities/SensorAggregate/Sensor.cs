using System;
using System.Collections.Generic;
using TechOnIt.Domain.Common;
using TechOnIt.Domain.Enums;

namespace TechOnIt.Domain.Entities.SensorAggregate;

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
        SensorType = Enumeration.FromDisplayName<SensorType>(typeName);
    }

    public void SetSensorType(int value)
    {
        SensorType = Enumeration.FromValue<SensorType>(value);
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
        Reports.Clear();
    }

    public void AddReport(PerformanceReport newReport)
    {
        Reports.Add(newReport);
    }

    public void RemoveReport(PerformanceReport newReport)
    {
        Reports.Remove(newReport);
    }

    public ICollection<PerformanceReport> GetReports()
        => Reports;
    #endregion

    #region relations & foreignkeys
    public Guid PlaceId { get; set; }

    public virtual ICollection<PerformanceReport> Reports { get; set; }
    #endregion
}
