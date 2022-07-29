using iot.Application.Common.Interfaces.Context;
using iot.Domain.Entities.Identity;

namespace iot.Application.Repositories.SQL.Users;

public class UserRepository : SqlRepository<User>, IUserRepository
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