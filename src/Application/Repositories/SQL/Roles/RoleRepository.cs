using iot.Infrastructure.Persistence.Context.Identity;

namespace iot.Application.Repositories.SQL.Roles;

public sealed class RoleRepository : IRoleRepository
{
    #region DI & Ctor's
    private readonly IIdentityContext _context;
    public RoleRepository(IIdentityContext context)
    {
        _context = context;
    }
    #endregion

}