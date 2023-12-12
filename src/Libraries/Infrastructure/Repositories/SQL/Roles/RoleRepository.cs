using TechOnIt.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using TechOnIt.Infrastructure.Persistence.Context;

namespace TechOnIt.Infrastructure.Repositories.SQL.Roles;

public sealed class RoleRepository : IRoleRepository
{
    #region DI & Ctor's
    private readonly IdentityContext _context;

    public RoleRepository(IdentityContext context)
    {
        _context = context;
    }
    #endregion


    public async Task CreateRoleAsync(RoleEntity role, CancellationToken cancellationToken = default)
    {
        await _context.Roles.AddAsync(role, cancellationToken);
        await Task.CompletedTask;
    }

    public async Task DeleteRoleByIdAsync(Guid roleId, CancellationToken cancellationToken = default)
    {
        var role = await _context.Roles.FindAsync(roleId, cancellationToken);
        if (role is null)
            await Task.FromException(new NullReferenceException());
        else
            _context.Roles.Remove(role);

        await Task.CompletedTask;
    }
    public async Task DeleteRoleAsync(RoleEntity role, CancellationToken cancellationToken = default)
    {
        _context.Roles.Remove(role);
        cancellationToken.ThrowIfCancellationRequested();
        await Task.CompletedTask;
    }

    public async Task DeleteRoleByNameAsync(string roleName, CancellationToken cancellationToken = default)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(a => a.Name == roleName, cancellationToken);
        if (role is null)
            await Task.FromException(new NullReferenceException());
        else
            _context.Roles.Remove(role);

        await Task.CompletedTask;
    }

    public async Task<IList<RoleEntity>?> GetRolesByUserId(Guid userId, CancellationToken cancellationToken)
        => await _context.Roles.Where(r => r.UserRoles != null && r.UserRoles.Any(ur => ur.UserId == userId)).ToListAsync(cancellationToken);

    public async Task UpdateRoleAsync(RoleEntity role, CancellationToken cancellationToken = default)
    {
        _context.Roles.Update(role);
        cancellationToken.ThrowIfCancellationRequested();
        await Task.CompletedTask;
    }

    public async Task<bool> IsExistsRoleNameAsync(string roleName, CancellationToken cancellationToken = default)
        => await _context.Roles.AnyAsync(a => a.Name == roleName, cancellationToken);

    public async Task<bool> HasSubsetUserAsync(Guid roleId, CancellationToken cancellationToken = default)
        => await _context.UserRoles.AnyAsync(a => a.RoleId == roleId);

    public async Task<RoleEntity?> FindRoleByIdAsync(Guid roleId, CancellationToken cancellationToken)
        => await _context.Roles.FirstOrDefaultAsync(a => a.Id == roleId, cancellationToken);

    public async Task<RoleEntity?> FindRoleByIdAsNoTrackingAsync(Guid roleId, CancellationToken cancellationToken)
        => await _context.Roles.AsNoTracking().FirstOrDefaultAsync(a => a.Id == roleId, cancellationToken);
}