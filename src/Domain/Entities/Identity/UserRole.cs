using System;

namespace iot.Domain.Entities.Identity
{
    public class UserRole
    {
        #region Constructors
        public UserRole(Guid userId, Guid roleId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            RoleId = roleId;
        }
        #endregion

        public Guid Id { get; set; }

        #region Relations
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
        #endregion
    }
}