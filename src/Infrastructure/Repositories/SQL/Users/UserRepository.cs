using iot.Domain.Entities.Identity.UserAggregate;
using iot.Infrastructure.Common.Encryptions;
using iot.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace iot.Application.Repositories.SQL.Users;

internal sealed class UserRepository : IUserRepository
{
    #region Constructor
    private readonly IdentityContext _context;
    private Encryptor aesEncryptor;

    public UserRepository(IdentityContext context)
    {
        _context = context;
        aesEncryptor = new Encryptor(GetUserKey());
    }
    #endregion

    public async Task<User?> FindUserByUserIdAsNoTrackingAsync(Guid userId, CancellationToken cancellationToken = default)
        => await _context.Users.AsNoTracking().FirstOrDefaultAsync(a => a.Id == userId, cancellationToken);

    public async Task<User?> FindUserByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    => await _context.Users.FirstOrDefaultAsync(a => a.Id == userId, cancellationToken);

    public async Task<User?> FindByUsernameAsync(string username, CancellationToken cancellationToken = default)
        => await _context.Users.AsNoTracking().FirstOrDefaultAsync(a => aesEncryptor.Decrypt(a.Username) == username.ToLower().Trim(), cancellationToken);

    public async Task<User?> FindByIdentityWithRolesAsync(string identity, CancellationToken stoppingToken = default)
    => await _context.Users
        .Where(u => u.Email == identity.Trim().ToLower() || u.PhoneNumber == identity.Trim())
        .Include(u => u.UserRoles)
        .ThenInclude(ur => ur.Role)
        .AsNoTracking()
        .FirstOrDefaultAsync(stoppingToken);

    public async Task<User?> FindUserByPhoneNumberWithRolesAsyncNoTracking(string phonenumber, CancellationToken cancellationToken = default)
    => await _context.Users
        .Where(u => u.PhoneNumber == phonenumber.Trim())
        .Include(u => u.UserRoles)
        .ThenInclude(ur => ur.Role)
        .AsNoTracking()
        .FirstOrDefaultAsync(cancellationToken);

    public async Task<IList<User>?> GetAllUsersByFilterAsync(Expression<Func<User, bool>>? filter = null,
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

    public async Task<bool> IsExistsUserByPhoneNumberAsync(string phonenumber, CancellationToken cancellationToken = default)
        => await _context.Users
            .AsNoTracking()
            .AnyAsync(a => a.PhoneNumber == phonenumber, cancellationToken);

    public async Task<bool> IsExistsUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
        => await _context.Users.AsNoTracking().AnyAsync(a => a.Id == userId, cancellationToken);

    public async Task<string> GetUserEmailByPhoneNumberAsync(string phonenumber, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.AsNoTracking().FirstAsync(a => aesEncryptor.Decrypt(a.PhoneNumber) == phonenumber);
        return user.Email;
    }

    public async Task<User?> CreateNewUser(User user, CancellationToken cancellationToken = default)
    {
        if (user.ConcurrencyStamp is null)
            user.RefreshConcurrencyStamp();

        var newUser = await _context.Users.AddAsync(user);
        return newUser.Entity;
    }

    public async Task DeleteUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var getUser = await _context.Users.FindAsync(userId, cancellationToken);

        getUser.SetIsDelete(true);
        getUser.SetIsBaned(true);
        getUser.RefreshConcurrencyStamp();

        _context.Users.Update(getUser);
        await Task.CompletedTask;
    }

    public async Task DeleteUserByPhoneNumberAsync(string phonenumber, CancellationToken cancellationToken = default)
    {
        var getUser = await _context.Users.FirstAsync(a => aesEncryptor.Decrypt(a.PhoneNumber) == phonenumber, cancellationToken);

        getUser.SetIsDelete(true);
        getUser.SetIsBaned(true);
        getUser.RefreshConcurrencyStamp();

        _context.Users.Update(getUser);
        await Task.CompletedTask;
    }

    public async Task UpdateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        var getUser = await _context.Users.FindAsync(user.Id, cancellationToken);

        getUser.SetEmail(user.Email);
        getUser.SetFullName(user.FullName);
        getUser.RefreshConcurrencyStamp();

        _context.Users.Update(user);
        await Task.CompletedTask;
    }

    #region privates
    private string GetUserKey()
    {
        var key = _context.AesKeys.AsNoTracking().First(a => a.Title == "UserKey");
        return key.Key;
    }
    #endregion
}