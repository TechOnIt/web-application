using iot.Domain.Common;
using System;

namespace iot.Domain.Entities.Identity;

public class UserRole : IEntity
{
    #region Constructors
    UserRole() { }

    public UserRole(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
    #endregion

    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }

    #region Relations
    public virtual User User { get; private set; }
    public virtual Role Role { get; private set; }
    #endregion
}