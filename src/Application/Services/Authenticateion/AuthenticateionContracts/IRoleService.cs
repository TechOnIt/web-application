using iot.Application.Common.Enums.IdentityServiceEnums;
using iot.Domain.Entities.Identity;

namespace iot.Application.Services.Authenticateion.AuthenticateionContracts;

public interface IRoleService
{
    Task<(IdentityCrudStatus Result, string Message)> CreateRoleAsync(string roleName, CancellationToken cancellationToken = default);
    Task<IdentityCrudStatus> DeleteRoleByIdAsync(Guid roleId,CancellationToken cancellationToken = default);
    Task<(IdentityCrudStatus Result, string Message)> UpdateRoleAsync(Guid roleId, string roleName, CancellationToken cancellationToken = default);
}