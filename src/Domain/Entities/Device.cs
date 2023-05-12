using TechOnIt.Domain.Entities.StructureAggregate;

namespace TechOnIt.Domain.Entities;

public class Device
{
    public Guid Id { get; private set; }
    public int Pin { get; private set; }
    public DeviceType Type { get; private set; } = DeviceType.Light;
    public bool IsHigh { get; set; } = false;
    public string ConcurrencyStamp { get; private set; } = string.Empty;
    #region relations & foreignkeys
    public Guid PlaceId { get; private set; }
    public virtual Place? Place { get; private set; } = null;
    #endregion

    #region Ctor
    private Device() { }
    public Device(int pin, DeviceType type, Guid placeId)
    {
        Id = Guid.NewGuid();
        SetPin(pin);
        Type = type;
        PlaceId = placeId;
    }
    #endregion

    #region methods
    /// <summary>
    /// Set pin value.
    /// </summary>
    /// <param name="pin">pin number you wanna to set.</param>
    public void SetPin(int pin)
    {
        if (pin <= 0 || pin > 255)
            throw new ArgumentOutOfRangeException($"Pin is out of range.");
        Pin = pin;
    }
    /// <summary>
    /// IsHigh = true
    /// </summary>
    public void High()
    {
        IsHigh = true;
    }
    /// <summary>
    /// IsHigh = false
    /// </summary>
    public void Low()
    {
        IsHigh = false;
    }
    /// <summary>
    /// Check row version is validate?
    /// </summary>
    public bool IsConcurrencyStampValidate(string concurrencyStamp)
        => ConcurrencyStamp == concurrencyStamp;
    #endregion
}
