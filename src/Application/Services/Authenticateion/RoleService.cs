﻿using iot.Application.Common.Enums.IdentityServiceEnums;
using iot.Application.Services.Authenticateion.AuthenticateionContracts;

namespace iot.Application.Services.Authenticateion;

public class RoleService : IRoleService
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public RoleService(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion

    public async Task<(IdentityCrudStatus Result, string Message)> CreateRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        bool isExistsBefor = 
            await _unitOfWorks.RoleRepository.IsExistsRoleNameAsync(roleName, cancellationToken);

        if (isExistsBefor)
            return (IdentityCrudStatus.Duplicate, "Duplicate: role name already exists in system");

        await _unitOfWorks.RoleRepository.CreateRoleAsync(roleName, cancellationToken);
        return (IdentityCrudStatus.Succeeded, "done successfully");
    }

    public async Task<IdentityCrudStatus> DeleteRoleByIdAsync(Guid roleId, CancellationToken cancellationToken)
    {
        bool hasUserInRole=await _unitOfWorks.RoleRepository.HasSubsetUserAsync(roleId,cancellationToken);
        if (hasUserInRole)
            return (IdentityCrudStatus.CantRemove);
        else
        {
            await _unitOfWorks.RoleRepository.DeleteRoleByIdAsync(roleId,cancellationToken);
            return IdentityCrudStatus.Succeeded;
        }
    }

    public async Task<(IdentityCrudStatus Result, string Message)> UpdateRoleAsync(Guid roleId,string roleName, CancellationToken cancellationToken)
    {
         var result=  _unitOfWorks.RoleRepository.UpdateRoleAsync(roleId, roleName, cancellationToken).IsCompletedSuccessfully;
        if (!result)
            return (IdentityCrudStatus.Failed, "An error occurred");

        return (IdentityCrudStatus.Succeeded, "done successfully");
    }

    #region privates
    #endregion
}