namespace iot.Application.Repositories.SQL.Roles;

public interface IRoleRepository
{
    Task CreateRoleAsync(string roleName,CancellationToken cancellationToken);
    Task DeleteRoleByIdAsync(Guid roleId,CancellationToken cancellationToken);
    Task DeleteRoleByNameAsync(string roleName,CancellationToken cancellationToken);
    Task UpdateRoleAsync(Guid roleId, string roleName,CancellationToken cancellationToken);
    Task<bool> IsExistsRoleNameAsync(string roleName,CancellationToken cancellationToken);
    Task<bool> HasSubsetUserAsync(Guid roleId, CancellationToken cancellationToken);
}