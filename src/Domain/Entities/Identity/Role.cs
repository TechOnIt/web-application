using System;

namespace iot.Domain.Entities.Identity
{
    public class Role
    {
        #region
        Role() { }

        public Role(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            NormalizedName = name.ToLower();
        }
        #endregion

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}