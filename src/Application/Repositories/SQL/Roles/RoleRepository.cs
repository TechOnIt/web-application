using iot.Infrastructure.Persistence.Context;

namespace iot.Application.Repositories.SQL.Roles;

public sealed class RoleRepository : SqlRepository<Role, IdentityContext>, IRoleRepository
{
    #region DI & Ctor's
    public RoleRepository(IdentityContext _context)
        : base(_context) { }
    #endregion

}