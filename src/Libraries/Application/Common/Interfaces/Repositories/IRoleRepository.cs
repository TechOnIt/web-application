using TechOnIt.Domain.Entities.Identities;

namespace TechOnIt.Application.Common.Interfaces.Repositories;

public interface IRoleRepository
{
    Task CreateRoleAsync(RoleEntity role, CancellationToken cancellationToken = default);
    Task DeleteRoleByIdAsync(Guid roleId, CancellationToken cancellationToken = default);
    Task DeleteRoleByNameAsync(string roleName, CancellationToken cancellationToken = default);
    Task UpdateRoleAsync(RoleEntity role, CancellationToken cancellationToken = default);
    Task<bool> IsExistsRoleNameAsync(string roleName, CancellationToken cancellationToken = default);
    Task<bool> HasSubsetUserAsync(Guid roleId, CancellationToken cancellationToken = default);
    Task<IList<RoleEntity>> GetRolesByUserId(Guid userId, CancellationToken cancellationToken);
    Task<RoleEntity> FindRoleByIdAsNoTrackingAsync(Guid roleId, CancellationToken cancellationToken);
    Task<RoleEntity> FindRoleByIdAsync(Guid roleId, CancellationToken cancellationToken);
    Task DeleteRoleAsync(RoleEntity role, CancellationToken cancellationToken = default);
}