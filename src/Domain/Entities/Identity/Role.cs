using System;
using System.Collections.Generic;

namespace TechOnIt.Domain.Entities.Identity;

public class Role
{
    #region Ctor
    private Role() { }

    public Role(string name)
    {
        Id = Guid.NewGuid();
        SetName(name);
    }
    #endregion

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string NormalizedName { get; private set; } = string.Empty;

    #region Methods
    public void SetName(string name)
    {
        name = name.Trim();
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException("Role name cannot be null or empty.");
        Name = name;
        NormalizedName = name.ToLower();
    }
    #endregion

    #region relations
    public virtual ICollection<UserRole>? UserRoles { get; set; }
    #endregion
}