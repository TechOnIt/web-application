using TechOnIt.Domain.Entities.StructureAggregate;

namespace TechOnIt.Domain.Entities.SensorAggregate;

public class Sensor
{
    public Guid Id { get; private set; }
    public int Pin { get; private set; }
    public SensorType Type { get; private set; } = SensorType.Thermometer;
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime? ModifiedAt { get; private set; }
    // Relations & Foreignkeys
    public Guid PlaceId { get; private set; }
    public virtual Place? Place { get; private set; }
    public virtual ICollection<SensorReport>? Reports { get; private set; }

    #region Ctor
    private Sensor() { }
    public Sensor(int pin, SensorType type, Guid placeId)
    {
        SetPin(pin);
        Type = type;
        PlaceId = placeId;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Set a pin number for sensor.
    /// </summary>
    /// <param name="pin">Pin number between 1-255.</param>
    public void SetPin(int pin)
    {
        if (pin <= 0) throw new ArgumentOutOfRangeException("Pin number must between 1-255.");

        Pin = pin;
    }
    #endregion

    #region Aggregate methods
    /// <summary>
    /// Add new report for this sensor.
    /// </summary>
    /// <param name="newReport">Sensor object model.</param>
    public void AddReport(SensorReport newReport)
    {
        if(Reports is null)
            throw new ArgumentNullException("Sensor reports list is null.");
        Reports.Add(newReport);
    }
    /// <summary>
    /// Remove a specific sensor report.
    /// </summary>
    /// <param name="newReport">Sensor report object model.</param>
    public void RemoveReport(SensorReport newReport)
    {
        Reports.Remove(newReport);
    }
    #endregion
}