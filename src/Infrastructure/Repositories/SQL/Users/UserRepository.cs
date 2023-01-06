using TechOnIt.Domain.Entities.Identity.UserAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechOnIt.Infrastructure.Persistence.Context;

namespace TechOnIt.Infrastructure.Repositories.SQL.Users;

internal sealed class UserRepository : IUserRepository
{
    #region Constructor
    private readonly IdentityContext _context;

    public UserRepository(IdentityContext context)
    {
        _context = context;
    }
    #endregion

    public async Task<User?> FindByIdAsNoTrackingAsync(Guid userId, CancellationToken cancellationToken = default)
        => await _context.Users.AsNoTracking().FirstOrDefaultAsync(a => a.Id == userId, cancellationToken);

    public async Task<User?> FindByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    => await _context.Users.FirstOrDefaultAsync(a => a.Id == userId, cancellationToken);

    public async Task<User?> FindByUsernameAsync(string username, CancellationToken cancellationToken = default)
        => await _context.Users.AsNoTracking().FirstOrDefaultAsync(a => a.Username == username.ToLower().Trim(), cancellationToken);

    public async Task<User?> FindByIdentityWithRolesAsync(string identity, CancellationToken stoppingToken = default)
    => await _context.Users
        .Where(u => u.Email == identity.Trim().ToLower() || u.PhoneNumber == identity.Trim())
        .Include(u => u.UserRoles)
        .ThenInclude(ur => ur.Role)
        .AsNoTracking()
        .FirstOrDefaultAsync(stoppingToken);

    public async Task<User?> FindByPhoneNumberWithRolesNoTrackingAsync(string phonenumber, CancellationToken cancellationToken = default)
    => await _context.Users
        .Where(u => u.PhoneNumber == phonenumber.Trim())
        .Include(u => u.UserRoles)
        .ThenInclude(ur => ur.Role)
        .AsNoTracking()
        .FirstOrDefaultAsync(cancellationToken);

    public async Task<User?> FindByUsernameWithRolesNoTrackingAsync(string username, CancellationToken cancellationToken = default)
    => await _context.Users
        .Where(u => u.Username == username.Trim())
        .Include(u => u.UserRoles)
        .ThenInclude(ur => ur.Role)
        .AsNoTracking()
        .FirstOrDefaultAsync(cancellationToken);

    public async Task<IList<User>?> GetAllByFilterAsync(Expression<Func<User, bool>>? filter = null,
        CancellationToken cancellationToken = default)
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

    public async Task<bool> IsExistsByPhoneNumberAsync(string phonenumber, CancellationToken cancellationToken = default)
        => await _context.Users
            .AsNoTracking()
            .AnyAsync(a => a.PhoneNumber == phonenumber, cancellationToken);

    public async Task<bool> IsExistsByIdAsync(Guid userId, CancellationToken cancellationToken = default)
        => await _context.Users.AsNoTracking().AnyAsync(a => a.Id == userId, cancellationToken);

    public async Task<bool> IsExistsByQueryAsync(Expression<Func<User, bool>> query, CancellationToken cancellationToken = default)
        => await _context.Users.AsNoTracking().AnyAsync(query, cancellationToken);

    public async Task<string> GetEmailByPhoneNumberAsync(string phonenumber, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.AsNoTracking().FirstAsync(a => a.PhoneNumber == phonenumber);
        return user.Email;
    }

    public async Task CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        if (user.ConcurrencyStamp is null)
            user.RefreshConcurrencyStamp();

        await _context.Users.AddAsync(user);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        var getUser = await _context.Users.FindAsync(user.Id, cancellationToken);

        getUser.SetEmail(user.Email);
        getUser.SetFullName(user.FullName);
        getUser.RefreshConcurrencyStamp();

        _context.Users.Update(user);
        await Task.CompletedTask;
    }

    public async Task RemoveAsync(User user, CancellationToken cancellationToken = default)
    {
        user.SetIsDelete(true);
        user.SetIsBaned(true);
        user.RefreshConcurrencyStamp();

        _context.Users.Update(user);
        await Task.CompletedTask;
    }
    public async Task DeleteByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var getUser = await _context.Users.FindAsync(userId, cancellationToken);

        getUser.SetIsDelete(true);
        getUser.SetIsBaned(true);
        getUser.RefreshConcurrencyStamp();

        _context.Users.Update(getUser);
        await Task.CompletedTask;
    }
    public async Task DeleteByPhoneNumberAsync(string phonenumber, CancellationToken cancellationToken = default)
    {
        var getUser = await _context.Users.FirstAsync(a => a.PhoneNumber == phonenumber, cancellationToken);

        getUser.SetIsDelete(true);
        getUser.SetIsBaned(true);
        getUser.RefreshConcurrencyStamp();

        _context.Users.Update(getUser);
        await Task.CompletedTask;
    }
}