using System;

namespace iot.Domain.Entities.Product.SensorAggregate;

public class PerformanceReport
{
    #region constructure
    public PerformanceReport(Guid id, int value, DateTime recordDateTime)
    {
        Id = id;
        Value = value;
        RecordDateTime = recordDateTime;
    }

    public PerformanceReport()
    {

    }
    #endregion

    public Guid Id { get; set; }
    public int Value { get; set; }

    private DateTime? _RecordDateTime;
    public DateTime RecordDateTime 
    {
        get { return _RecordDateTime??DateTime.Now; }
        set { _RecordDateTime = value; } 
    }

    #region relations
    public Guid SensorId { get; set; }
    public Sensor Sensor { get; set; }
    #endregion
}
