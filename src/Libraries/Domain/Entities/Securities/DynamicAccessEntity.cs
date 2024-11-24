using TechOnIt.Domain.Entities.Identities;
using TechOnIt.Domain.Entities.Identities.UserAggregate;

namespace TechOnIt.Domain.Entities.Securities;

public class DynamicAccessEntity
{
    #region Ctor
    DynamicAccessEntity() { }
    public DynamicAccessEntity(string path, Guid userId, Guid roleId)
    {
        SetPath(path);

        if (userId == Guid.Empty) throw new ArgumentNullException("User id cannot be empty.");
        UserId = userId;

        if (roleId == Guid.Empty) throw new ArgumentNullException("Role id cannot be empty.");
        RoleId = roleId;
    }
    #endregion

    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Path { get; private set; } = string.Empty;
    public DateTime CreatedOn { get; private set; } = DateTime.Now;
    public Guid UserId { get; private set; } = Guid.Empty;
    public Guid RoleId { get; private set; } = Guid.Empty;

    #region Relations
    public virtual UserEntity? User { get; private set; }
    public virtual RoleEntity? Role { get; private set; }
    #endregion

    #region Methods
    private void SetPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentNullException(nameof(path));
        Path = path;
    }
    #endregion
}