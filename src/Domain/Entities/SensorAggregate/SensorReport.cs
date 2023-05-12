namespace TechOnIt.Domain.Entities.SensorAggregate;

public class SensorReport
{
    public Guid Id { get; private set; }
    public double Value { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    public Guid SensorId { get; private set; }
    public virtual Sensor? Sensor { get; private set; }

    #region Ctor
    private SensorReport() { }
    public SensorReport(double value, Guid sensorId)
    {
        SensorId = sensorId;
        Value = value;
    }
    #endregion
}