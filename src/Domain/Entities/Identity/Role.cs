using System;

namespace iot.Domain.Entities.Identity;

public class Role
{
    #region
    Role() { }

    public Role(string name)
    {
        Id = Guid.NewGuid();
        SetName(name);
    }
    #endregion

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string NormalizedName { get; private set; }

    #region Methods
    public void SetName(string name)
    {
        name = name.Trim();
        if(string.IsNullOrEmpty(name))
            throw new ArgumentNullException("Role name cannot be null or empty.");
        Name = name;
        NormalizedName = name.ToLower();
    }
    #endregion
}