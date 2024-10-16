using TechOnIt.Domain.Entities.Catalog;

namespace TechOnIt.Domain.Entities.SensorAggregate;

public class SensorEntity : BaseEntity, ICreateable
{
    public int Pin { get; private set; }
    public SensorType Type { get; private set; } = SensorType.Thermometer;
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    // Relations & Foreignkeys
    public Guid GroupId { get; private set; }
    public virtual Group? Group { get; private set; }
    public virtual ICollection<SensorReportEntity>? Reports { get; private set; }

    #region Ctor
    private SensorEntity() { }
    public SensorEntity(int pin, SensorType type, Guid groupId)
    {
        SetPin(pin);
        Type = type;
        GroupId = groupId;
    }
    #endregion

    #region Methods

    /// <summary>
    /// Set a pin number for sensor.
    /// </summary>
    /// <param name="pin">Pin number between 1-255.</param>
    public void SetPin(int pin)
    {
        if (pin <= 0 || pin > 255) throw new ArgumentOutOfRangeException("Pin number must between 1-255.");

        Pin = pin;
    }


    #endregion

    #region Aggregate methods

    /// <summary>
    /// Add new report for this sensor.
    /// </summary>
    /// <param name="newReport">Sensor object model.</param>
    public void AddReport(SensorReportEntity newReport)
    {
        if(Reports is null)
            throw new ArgumentNullException("Sensor reports list is null.");
        Reports.Add(newReport);
    }
    /// <summary>
    /// Remove a specific sensor report.
    /// </summary>
    /// <param name="newReport">Sensor report object model.</param>
    public void RemoveReport(SensorReportEntity newReport)
    {
        Reports.Remove(newReport);
    }

    #endregion
}