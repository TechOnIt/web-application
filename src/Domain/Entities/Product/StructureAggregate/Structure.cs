using iot.Domain.Enums;
using iot.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iot.Domain.Entities.Product.StructureAggregate;

public class Structure // this class is aggregate root of Structure aggregate
{

    #region immutable constructure
    public Structure(Guid id, string name, string description, DateTime createDate, DateTime? modifyDate, StuctureType structureType)
    {
        Id = id;
        Name = name;
        ApiKey = Concurrency.NewToken();
        Description = description;
        IsActive = true;
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
    public DateTime? ModifyDate { get;private set; }

    #region immutable options

    public void SetStructureType(StuctureType newType)
    {
        this.Type = newType;
    }

    public void SetStructureType(string typeName)
    {
        this.Type = Common.Enumeration.FromDisplayName<StuctureType>(typeName);
    }

    public void SetStructureType(int value)
    {
        this.Type = Common.Enumeration.FromValue<StuctureType>(value);
    }

    public StuctureType GetStuctureType()
        => this.Type;

    public void SetApiKey()
    {
        this.ApiKey = Concurrency.NewToken();
    }

    public void SetModifyDate()
    {
        this.ModifyDate = DateTime.Now;
    }
    #endregion

    #region aggregate methods and operations for place entity
    public Place NewPlace(Guid id, string name, string description, DateTime createdate, DateTime modifydate, Guid structureId)
    {
        return new Place(id,name,description,createdate,modifydate,structureId);
    }

    public Place NewPlace()
    {
        return new Place();
    }

    public void AddPlace(Place place)
    {
        Places.Add(place);
    }

    public void RemovePlace(Place place)
    {
        Places.Remove(place);
    }

    public void SetPlaceModifyDate(Place place)
    {
        var getPlace = Places.FirstOrDefault(a=>a.Id==place.Id);
        if (getPlace is not null)
        {
            place.SetModifyDate();
        }
    }

    public void AddRangePlace(ICollection<Place> places)
    {
        foreach (var item in places)
        {
            Places.Add(item);
        }
    }

    #endregion

    #region relations & foreign keys
    public Guid UserId { get; set; }
    public virtual ICollection<Place> Places { get; set; }
    #endregion
}
