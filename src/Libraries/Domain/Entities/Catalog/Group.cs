using TechOnIt.Domain.Entities.Controllers;
using TechOnIt.Domain.Entities.SensorAggregate;

namespace TechOnIt.Domain.Entities.Catalog;

public class Group : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    #region Ctor
    private Group() { }
    public Group(string name, Guid structureId)
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
    /// Set name for group.
    /// </summary>
    /// <param name="name">Group name</param>
    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException("Group name cannot be null.");
        Name = name;
    }
    #endregion
}