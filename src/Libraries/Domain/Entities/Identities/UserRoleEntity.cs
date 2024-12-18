﻿using TechOnIt.Domain.Entities.Identities.UserAggregate;

namespace TechOnIt.Domain.Entities.Identities;

public class UserRoleEntity
{
    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }

    #region Relations
    public virtual UserEntity? User { get; private set; }
    public virtual RoleEntity? Role { get; private set; }
    #endregion

    #region Ctor
    private UserRoleEntity() { }
    public UserRoleEntity(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
    #endregion
}