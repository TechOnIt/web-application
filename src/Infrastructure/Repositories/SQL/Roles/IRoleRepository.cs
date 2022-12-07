using TechOnIt.Domain.Entities.Identity;

namespace TechOnIt.Infrastructure.Repositories.SQL.Roles;

public interface IRoleRepository
{
    Task CreateRoleAsync(Role role, CancellationToken cancellationToken = default);
    Task DeleteRoleByIdAsync(Guid roleId, CancellationToken cancellationToken = default);
    Task DeleteRoleByNameAsync(string roleName, CancellationToken cancellationToken = default);
    Task UpdateRoleAsync(Guid roleId, string roleName, CancellationToken cancellationToken = default);
    Task<bool> IsExistsRoleNameAsync(string roleName, CancellationToken cancellationToken = default);
    Task<bool> HasSubsetUserAsync(Guid roleId, CancellationToken cancellationToken = default);
}