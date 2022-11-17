using iot.Domain.Entities.Identity;
using iot.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace iot.Application.Repositories.SQL.Roles;

public sealed class RoleRepository : IRoleRepository
{
    #region DI & Ctor's
    private readonly IdentityContext _context;

    public RoleRepository(IdentityContext context)
    {
        _context = context;
    }
    #endregion


    public async Task CreateRoleAsync(string roleName, CancellationToken cancellationToken = default)
    {
        await _context.Roles.AddAsync(new Role(name: roleName), cancellationToken);
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