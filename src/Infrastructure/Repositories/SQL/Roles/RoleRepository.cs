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


    public async Task CreateRoleAsync(Role role, CancellationToken cancellationToken = default)
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

    public async Task DeleteRoleByNameAsync(string roleName, CancellationToken cancellationToken = default)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(a => a.Name == roleName, cancellationToken);
        if (role is null)
            await Task.FromException(new NullReferenceException());
        else
            _context.Roles.Remove(role);

        await Task.CompletedTask;
    }

    public async Task<IList<Role>?> GetRolesByUserId(Guid userId,CancellationToken cancellationToken)
    {
        var userRoles = await (from ur in _context.UserRoles
                               join r in _context.Roles on ur.RoleId equals r.Id
                               where ur.UserId == userId
                               select new Role 
                               { 
                                   Id = r.Id,
                                   Name = r.Name,
                                   NormalizedName = r.NormalizedName 
                               }).ToListAsync(cancellationToken);

        return await Task.FromResult<IList<Role>?>(userRoles);
    }

    public async Task UpdateRoleAsync(Guid roleId, string roleName, CancellationToken cancellationToken = default)
    {
        var getrole = await _context.Roles.FindAsync(roleId, cancellationToken);
        if (getrole is null)
        {
            await Task.FromException(new NullReferenceException());
        }
        else
        {
            getrole.SetName(roleName);
            await Task.CompletedTask;
        }
    }

    public async Task<bool> IsExistsRoleNameAsync(string roleName, CancellationToken cancellationToken = default)
        => await _context.Roles.AnyAsync(a => a.Name == roleName, cancellationToken);

    public async Task<bool> HasSubsetUserAsync(Guid roleId, CancellationToken cancellationToken = default)
        => await _context.UserRoles.AnyAsync(a => a.RoleId == roleId);
}