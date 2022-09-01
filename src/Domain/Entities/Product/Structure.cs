using iot.Domain.Enums;
using iot.Domain.ValueObjects;
using System;
using System.Collections;
using System.Collections.Generic;

namespace iot.Domain.Entities.Product;

public class Structure
{

    #region immutable constructure
    public Structure(Guid id, Concurrency apiKey, string description, bool isActive, DateTime createDate, DateTime? modifyDate, StuctureType structureType)
    {
        Id = id;
        ApiKey = apiKey;
        Description = description;
        IsActive = isActive;
        CreateDate = createDate;
        ModifyDate = modifyDate;
        Type = structureType;
    }

    public Structure()
    {
        CreateDate = DateTime.Now;
    }
    #endregion

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public StuctureType Type { get; private set; }
    public Concurrency ApiKey { get; private set; }
    public DateTime CreateDate { get; private set; }
    public DateTime? ModifyDate { get; set; }

    #region immutable options

    public void SetStructureType(StuctureType newType)
    {
        Type = newType;
    }

    public void SetApiKey()
    {
        ApiKey = Concurrency.NewToken();
    }
    #endregion

    #region relations & foreign keys
    public Guid UserId { get; set; }
    public virtual ICollection<Place> Places { get; set; }
    #endregion
}
