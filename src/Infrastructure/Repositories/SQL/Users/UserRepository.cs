﻿using iot.Domain.Entities.Identity;
using iot.Infrastructure.Persistence.Context.Identity;
using Microsoft.EntityFrameworkCore;
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

    public async Task<User?> FindUserByUserIdAsNoTrackingAsync(Guid userId)
        => await _context.Users.AsNoTracking().FirstOrDefaultAsync(a => a.Id == userId);
    public async Task<User?> FindByUsernameAsync(string username, CancellationToken cancellationToken = default)
        => await _context.Users.AsNoTracking().FirstOrDefaultAsync(a => a.Username.ToLower().Trim() == username.ToLower().Trim(), cancellationToken);

    /// <summary>
    /// Find user by email or phone number with roles (AsNoTracking).
    /// </summary>
    /// <param name="identity">Email or Phone number</param>
    /// <returns>An specific user.</returns>
    public async Task<User?> FindByIdentityWithRolesAsync(string identity, CancellationToken stoppingToken = default)
    => await _context.Users
        .Where(u => u.Email == identity.Trim().ToLower() || u.PhoneNumber == identity.Trim())
        .Include(u => u.UserRoles)
        .ThenInclude(ur => ur.Role)
        .AsNoTracking()
        .FirstOrDefaultAsync(stoppingToken);

    public async Task<User?> FindUserByPhoneNumberWithRolesAsyncNoTracking(string phonenumber,CancellationToken cancellationToken)
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

    public async Task<bool> IsExistsUserByPhoneNumberAsync(string phonenumber)
        => await _context.Users.AsNoTracking().AnyAsync(a => a.PhoneNumber == phonenumber);

    public async Task<string> GetUserEmailByPhoneNumberAsync(string phonenumber)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(a=>a.PhoneNumber==phonenumber);
        return user.Email;
    }

    public async Task<User> CreateNewUser(User user,CancellationToken cancellationToken)
    {
        if(user.ConcurrencyStamp is null)
            user.RefreshConcurrencyStamp();
        
        var newUser= await _context.Users.AddAsync(user);
        return newUser.Entity;
    }
}