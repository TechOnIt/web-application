using iot.Application.Common.Interfaces.Context;
using iot.Application.Common.Interfaces.Dependency;

namespace iot.Application.Repositories.SQL.Roles;

public sealed class RoleRepository : SqlRepository<Role>, IRoleRepository, IScopedDependency
{
    #region DI & Ctor's
    public RoleRepository(IIdentityContext _context)
        : base(_context) { }
    #endregion
}