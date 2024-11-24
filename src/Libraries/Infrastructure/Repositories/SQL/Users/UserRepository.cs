using TechOnIt.Domain.Entities.Identities.UserAggregate;

namespace TechOnIt.Infrastructure.Repositories.SQL.Users;

internal sealed class UserRepository : IUserRepository
{
    #region Constructor
    private readonly IAppDbContext _context;

    public UserRepository(IAppDbContext context)
    {
        _context = context;
    }
    #endregion

    public async Task<UserEntity?> FindByIdAsNoTrackingAsync(Guid userId, CancellationToken cancellationToken = default)
        => await _context.Users.AsNoTracking().FirstOrDefaultAsync(a => a.Id == userId, cancellationToken);

    public async Task<UserEntity?> FindByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    => await _context.Users.FirstOrDefaultAsync(a => a.Id == userId, cancellationToken);

    public async Task<UserEntity?> FindByUsernameAsync(string username, CancellationToken cancellationToken = default)
        => await _context.Users.AsNoTracking().FirstOrDefaultAsync(a => a.Username == username.ToLower().Trim(), cancellationToken);

    public async Task<UserEntity?> FindByIdentityWithRolesAsync(string identity, CancellationToken stoppingToken = default)
    => await _context.Users
        .Where(u => u.Email == identity.Trim().ToLower() || u.PhoneNumber == identity.Trim())
        .Include(u => u.UserRoles)
        .ThenInclude(ur => ur.Role)
        .AsNoTracking()
        .FirstOrDefaultAsync(stoppingToken);

    public async Task<UserEntity?> FindByPhoneNumberWithRolesNoTrackingAsync(string phonenumber, CancellationToken cancellationToken = default)
    => await _context.Users
        .Where(u => u.PhoneNumber == phonenumber.Trim())
        .Include(u => u.UserRoles)
        .ThenInclude(ur => ur.Role)
        .AsNoTracking()
        .FirstOrDefaultAsync(cancellationToken);

    public async Task<UserEntity?> FindByUsernameWithRolesNoTrackingAsync(string username, CancellationToken cancellationToken = default)
    => await _context.Users
        .Where(u => u.Username == username.Trim())
        .Include(u => u.UserRoles)
        .ThenInclude(ur => ur.Role)
        .AsNoTracking()
        .FirstOrDefaultAsync(cancellationToken);
    public async Task<IList<UserEntity>?> GetAllByFilterAsync(Expression<Func<UserEntity, bool>>? filter = null,
        CancellationToken cancellationToken = default)
    {
        if (filter != null)
        {
            IQueryable<UserEntity> query = _context.Users;
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
    public async Task<bool> IsExistsByQueryAsync(Expression<Func<UserEntity, bool>> query, CancellationToken cancellationToken = default)
        => await _context.Users.AsNoTracking().AnyAsync(query, cancellationToken);
    public async Task<string> GetEmailByPhoneNumberAsync(string phonenumber, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.AsNoTracking().FirstAsync(a => a.PhoneNumber == phonenumber);
        return user.Email;
    }
    public async Task CreateAsync(UserEntity user, CancellationToken cancellationToken)
        => await _context.Users.AddAsync(user, cancellationToken);
    public async Task UpdateAsync(UserEntity user, CancellationToken cancellationToken)
    {
        var getUser = await _context.Users.FindAsync(user.Id, cancellationToken);
        getUser.SetEmail(user.Email);
        getUser.SetFullName(user.FullName);
        cancellationToken.ThrowIfCancellationRequested();
        _context.Users.Update(user);
        await Task.CompletedTask;
    }
    public async Task RemoveAsync(UserEntity user, CancellationToken cancellationToken = default)
    {
        user.Delete();
        user.Ban();
        _context.Users.Update(user);
        await Task.CompletedTask;
    }
    public async Task DeleteByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var getUser = await _context.Users.FindAsync(userId, cancellationToken);
        if (getUser is null)
            return;
        getUser.Delete();
        _context.Users.Update(getUser);
        await Task.CompletedTask;
    }
    public async Task DeleteByPhoneNumberAsync(string phonenumber, CancellationToken cancellationToken = default)
    {
        var getUser = await _context.Users.FirstAsync(a => a.PhoneNumber == phonenumber, cancellationToken);
        getUser.Delete();
        _context.Users.Update(getUser);
        await Task.CompletedTask;
    }
}