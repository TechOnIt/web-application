using TechOnIt.Domain.Common;
using System;
using System.Collections.Generic;

namespace TechOnIt.Domain.Entities.Identity;

public class Role
{
    #region Ctor
    public Role() { }

    public Role(string name)
    {
        Id = Guid.NewGuid();
        SetName(name);
    }
    #endregion

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string NormalizedName { get; set; }

    #region Methods
    public void SetName(string name)
    {
        name = name.Trim();
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException("Role name cannot be null or empty.");
        Name = name;
        NormalizedName = name.ToLower();
    }
    #endregion

    #region relations
    public virtual ICollection<UserRole> UserRoles { get; set; }
    #endregion
}