namespace TechOnIt.Domain.Entities.SensorAggregate;

public class SensorReportEntity : BaseEntity
{
    public double Value { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    public Guid SensorId { get; private set; }
    public virtual SensorEntity? Sensor { get; private set; }

    #region Ctor
    private SensorReportEntity() { }
    public SensorReportEntity(double value, Guid sensorId)
    {
        SensorId = sensorId;
        Value = value;
    }
    #endregion
}