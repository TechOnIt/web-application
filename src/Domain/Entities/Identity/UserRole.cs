using TechOnIt.Domain.Entities.Identity.UserAggregate;

namespace TechOnIt.Domain.Entities.Identity;

public class UserRole
{
    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }

    #region Relations
    public virtual User? User { get; private set; }
    public virtual Role? Role { get; private set; }
    #endregion

    #region Ctor
    private UserRole() { }
    public UserRole(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
    #endregion
}