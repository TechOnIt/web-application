using TechOnIt.Domain.Entities.SensorAggregate;

namespace TechOnIt.Domain.Entities.StructureAggregate;

public class Place : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    #region Ctor
    private Place() { }
    public Place(string name, Guid structureId)
    {
        SetName(name);
        StructureId = structureId;
    }
    #endregion

    #region Relations and Foreignkeys
    public Guid StructureId { get; private set; }
    public virtual Structure? Structure { get; private set; }
    public virtual ICollection<RelayEntity>? Relays { get; set; }
    public virtual ICollection<SensorEntity>? Sensors { get; private set; }
    #endregion

    #region Methods
    /// <summary>
    /// Set name for place.
    /// </summary>
    /// <param name="name">Place name</param>
    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException("Place name cannot be null.");
        Name = name;
    }
    #endregion
}