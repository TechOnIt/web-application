﻿using TechOnIt.Domain.Entities.Catalogs;

namespace TechOnIt.Domain.Entities.Controllers;

public class RelayEntity
{
    public Guid Id { get; private set; }
    public int Pin { get; private set; }
    public RelayType Type { get; private set; } = RelayType.Light;
    public bool IsHigh { get; set; } = false;
    public byte[] ConcurrencyStamp { get; private set; } = new byte[0];
    #region relations & foreignkeys
    public Guid GroupId { get; private set; }
    public virtual GroupEntity? Group { get; private set; } = null;
    #endregion

    #region Ctor
    private RelayEntity() { }
    public RelayEntity(int pin, RelayType type, Guid groupId)
    {
        Id = Guid.NewGuid();
        SetPin(pin);
        Type = type;
        GroupId = groupId;
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
        => ConcurrencyStamp == Encoding.ASCII.GetBytes(concurrencyStamp);
    #endregion
}
