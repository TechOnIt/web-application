namespace TechOnIt.Domain.Entities.Securities;

/// <summary>
/// Represents a permission record
/// </summary>
public class PermissionEntity
{
    public PermissionEntity(string systemName)
    {
        SystemName = systemName;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the permission name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the permission system name
    /// </summary>
    public string SystemName { get; private set; }
}