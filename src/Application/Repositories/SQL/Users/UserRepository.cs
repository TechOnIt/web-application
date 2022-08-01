using iot.Application.Common.Interfaces.Context;
using iot.Application.Common.Interfaces.Dependency;
using iot.Domain.Entities.Identity;

namespace iot.Application.Repositories.SQL.Users;

internal sealed class UserRepository : SqlRepository<User>, IUserRepository, IScopedDependency
{
    #region DI & Ctor's
    // Uncomment when need.
    // private readonly IIdentityContext _context;
    public UserRepository(IIdentityContext context)
        : base(context)
    {
        //_context = context;
    }
    #endregion
}