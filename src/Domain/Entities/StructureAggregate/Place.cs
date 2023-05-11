using System;
using System.Collections.Generic;

namespace TechOnIt.Domain.Entities.StructureAggregate;

public class Place
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime? ModifyDate { get; private set; }

    #region Ctor
    private Place() { }
    public Place(string name, Guid structureId)
    {
        SetName(name);
        StructureId = structureId;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Add description for place.
    /// </summary>
    /// <param name="description">Description content.</param>
    public Place SetDescription(string description)
    {
        if (!string.IsNullOrEmpty(description) && !string.IsNullOrWhiteSpace(description))
        {
            Description = description;
            ModifyDate = DateTime.Now;
        }
        return this;
    }

    /// <summary>
    /// Set name for place.
    /// </summary>
    /// <param name="name">Place name</param>
    private void SetName(string name)
    {
        if(string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException("Place name cannot be null.");
        Name = name;
    }

    /// <summary>
    /// Change place name.
    /// </summary>
    /// <param name="name">Place new name you want to change.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public Place ChangeName(string name)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Place name cannot be null.");
        Name = name;
        ModifyDate = DateTime.Now;
        return this;
    }
    #endregion

    #region Relations and Foreignkeys
    public Guid StructureId { get; set; }
    public virtual Structure? Structure { get; set; }

    public virtual ICollection<Device>? Devices { get; set; }
    #endregion
}