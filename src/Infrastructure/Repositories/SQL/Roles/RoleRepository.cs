using iot.Infrastructure.Persistence.Context.Identity;

namespace iot.Application.Repositories.SQL.Roles;

public sealed class RoleRepository : IRoleRepository
{
    #region DI & Ctor's
    private readonly IdentityContext _context;
    public RoleRepository(IdentityContext context)
    {
        _context = context;
    }
    #endregion

}