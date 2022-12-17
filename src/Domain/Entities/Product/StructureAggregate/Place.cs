using System;
using System.Collections.Generic;

namespace TechOnIt.Domain.Entities.Product.StructureAggregate;

public class Place
{
    #region constructor
    public Place(Guid id, string name, string description, DateTime createdate, DateTime modifydate, Guid structureId)
    {
        Id = id;
        Name = name;
        Description = description;
        CreateDate = createdate;
        ModifyDate = modifydate;
        StuctureId = structureId;
    }

    public Place()
    {
    }
    #endregion

    #region methods
    public void SetModifyDate()
    {
        ModifyDate = DateTime.Now;
    }

    internal DateTime GetModifyDate()
    {
        return (DateTime)ModifyDate;
    }

    public void SetCreateDate() => this.CreateDate = DateTime.Now;
    #endregion

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }


    private DateTime? _CreateDate;
    public DateTime CreateDate
    {
        get { return _CreateDate ?? DateTime.Now; }
        private set { _CreateDate = value; }
    }


    public DateTime? ModifyDate { get; private set; }

    #region relations and foreignkeys
    public Guid StuctureId { get; set; }
    public virtual Structure Structure { get; set; }

    public virtual ICollection<Device> Devices { get; set; }
    #endregion
}
