using TechOnIt.Domain.Entities.Identity;

namespace TechOnIt.Infrastructure.Repositories.SQL.Roles;

public interface IRoleRepository
{
    Task CreateRoleAsync(Role role, CancellationToken cancellationToken = default);
    Task DeleteRoleByIdAsync(Guid roleId, CancellationToken cancellationToken = default);
    Task DeleteRoleByNameAsync(string roleName, CancellationToken cancellationToken = default);
    Task UpdateRoleAsync(Role role, CancellationToken cancellationToken = default);
    Task<bool> IsExistsRoleNameAsync(string roleName, CancellationToken cancellationToken = default);
    Task<bool> HasSubsetUserAsync(Guid roleId, CancellationToken cancellationToken = default);
    Task<IList<Role>?> GetRolesByUserId(Guid userId, CancellationToken cancellationToken);
    Task<Role?> FindRoleByIdAsNoTrackingAsync(Guid roleId, CancellationToken cancellationToken);
    Task<Role?> FindRoleByIdAsync(Guid roleId, CancellationToken cancellationToken);
    Task DeleteRoleAsync(Role role, CancellationToken cancellationToken = default);
}