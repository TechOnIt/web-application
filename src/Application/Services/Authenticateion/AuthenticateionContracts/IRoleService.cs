using iot.Application.Common.Enums.IdentityService;
using iot.Domain.Entities.Identity;

namespace iot.Application.Services.Authenticateion.AuthenticateionContracts;

public interface IRoleService
{
    Task<(IdentityCrudStatus Result, string Message)> CreateRoleAsync(string roleName, CancellationToken cancellationToken);
    Task<IdentityCrudStatus> DeleteRoleByIdAsync(Guid roleId,CancellationToken cancellationToken);
    Task<(IdentityCrudStatus Result, string Message)> UpdateRoleAsync(Guid roleId, string roleName, CancellationToken cancellationToken);
}