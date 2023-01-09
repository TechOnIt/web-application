using TechOnIt.Application.Common.Enums.IdentityService;

namespace TechOnIt.Application.Services.Authenticateion.AuthenticateionContracts;

public interface IRoleService
{
    Task<(IdentityCrudStatus Result, string Message)> CreateRoleAsync(Role role, CancellationToken cancellationToken = default);
    Task<IdentityCrudStatus> DeleteRoleByIdAsync(Guid roleId, CancellationToken cancellationToken = default);
}