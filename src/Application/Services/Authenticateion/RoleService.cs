using TechOnIt.Application.Common.Enums.IdentityService;
using TechOnIt.Application.Services.Authenticateion.AuthenticateionContracts;

namespace TechOnIt.Application.Services.Authenticateion;

public class RoleService : IRoleService
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public RoleService(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion

    public async Task<(IdentityCrudStatus Result, string Message)> CreateRoleAsync(Role role, CancellationToken cancellationToken = default)
    {
        bool isExistsBefor =
            await _unitOfWorks.RoleRepository.IsExistsRoleNameAsync(role.Name, cancellationToken);

        if (isExistsBefor)
            return (IdentityCrudStatus.Duplicate, "Duplicate: role name already exists in system");

        await _unitOfWorks.RoleRepository.CreateRoleAsync(role, cancellationToken);
        return (IdentityCrudStatus.Succeeded, "done successfully");
    }

    public async Task<IdentityCrudStatus> DeleteRoleByIdAsync(Guid roleId, CancellationToken cancellationToken = default)
    {
        bool hasUserInRole = await _unitOfWorks.RoleRepository.HasSubsetUserAsync(roleId, cancellationToken);
        if (hasUserInRole)
            return IdentityCrudStatus.CantRemove;
        else
        {
            await _unitOfWorks.RoleRepository.DeleteRoleByIdAsync(roleId, cancellationToken);
            return IdentityCrudStatus.Succeeded;
        }
    }

    #region privates
    #endregion
}
