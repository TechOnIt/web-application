using System;

namespace iot.Domain.Entities.Identity
{
    public class UserRole
    {
        #region Constructors
        UserRole() { }

        public UserRole(Guid userId, Guid roleId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            RoleId = roleId;
        }
        #endregion

        public Guid Id { get; private set; }

        #region Relations
        public Guid UserId { get; private set; }
        public Guid RoleId { get; private set; }

        public virtual User User { get; private set; }
        public virtual Role Role { get; private set; }
        #endregion
    }
}