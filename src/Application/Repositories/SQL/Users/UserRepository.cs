using iot.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace iot.Application.Repositories.SQL.Users;

internal sealed class UserRepository : IUserRepository
{
    #region Constructor
    private readonly IdentityContext _context;
    public UserRepository(IdentityContext context)
    {
        _context = context;
    }
    #endregion

    public async Task<User?> FindByUsernameAsync(string username, CancellationToken cancellationToken = default)
        => await _context.Users.AsNoTracking().FirstOrDefaultAsync(a => a.Username.ToLower().Trim() == username.ToLower().Trim(), cancellationToken);

    public async Task<IList<User>> GetAllUsersByFilterAsync(Expression<Func<User, bool>> filter = null, CancellationToken cancellationToken = default)
    {
        if (filter != null)
        {
            IQueryable<User> query = _context.Users;
            query = query.Where(filter);

            return await query.AsNoTracking().ToListAsync(cancellationToken);
        }
        else
        {
            return await _context.Users.AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}