using iot.Application.Common.Interfaces.Context;
using iot.Application.Common.Interfaces.Dependency;

namespace iot.Application.Repositories.SQL.Users;

internal sealed class UserRepository : SqlRepository<User>, IUserRepository, IScopedDependency
{
    #region DI & Ctor's
    private readonly IIdentityContext _context;
    public UserRepository(IIdentityContext context)
        : base(context)
    {
        _context = context;
    }
    #endregion

    public async Task<User?> FindByUsernameAsync(string username, CancellationToken cancellationToken = default)
        => await TableNoTracking.FirstOrDefaultAsync(user => user.Username == username, cancellationToken);
}