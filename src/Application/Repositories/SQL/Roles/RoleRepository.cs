using iot.Application.Common.Interfaces.Context;

namespace iot.Application.Repositories.SQL.Roles;

public class RoleRepository : SqlRepository<Role>, IRoleRepository
{
    #region DI & Ctor's
    public RoleRepository(IIdentityContext _context)
        : base(_context) { }
    #endregion
}