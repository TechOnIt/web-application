using System.Security.Claims;
using TechOnIt.Application.Common.Enums.IdentityService;
using TechOnIt.Application.Common.Models.DTOs.Claims;

namespace TechOnIt.Application.Common.Extentions;

public static class ClaimsExtentions
{

    #region user
    public static async Task<(Guid Id, IdentityCurrentType Type)> GetCurrentUserIdAsync(this System.Security.Claims.ClaimsPrincipal claims)
    {
        try
        {
            if (claims is null || claims.Identity is null || !claims.Identity.IsAuthenticated)
                return await Task.FromResult((Guid.Empty, IdentityCurrentType.NotAuthenticated));

            if (claims.Identity.FindFirstValue(ClaimTypes.MobilePhone) == null)
                return await Task.FromResult((Guid.Empty, IdentityCurrentType.Structure));

            return await Task.FromResult((Guid.Parse(claims.Identity.FindFirstValue(ClaimTypes.NameIdentifier)), IdentityCurrentType.User));
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }

    public static async Task<UserClaims?> GetCurrentUserAsync(this System.Security.Claims.ClaimsPrincipal claims)
    {
        try
        {
            if (claims is null || claims.Identity is null || !claims.Identity.IsAuthenticated || string.IsNullOrWhiteSpace(ClaimTypes.MobilePhone) is true)
                return await Task.FromResult<UserClaims?>(null);

            return await Task.FromResult(new UserClaims(Guid.Parse(claims.Identity.FindFirstValue(ClaimTypes.NameIdentifier)), claims.Identity.FindFirstValue(ClaimTypes.Name)
                , claims.Identity.FindFirstValue(ClaimTypes.MobilePhone), claims.Identity.FindFirstValue(ClaimTypes.Role)));
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }

    #endregion

    #region structure
    public static async Task<StructureClaims?> GetCurrentStructureAsync(this ClaimsPrincipal claims)
    {
        try
        {
            if (claims is null || claims.Identity is null || !claims.Identity.IsAuthenticated || string.IsNullOrWhiteSpace(ClaimTypes.SerialNumber) is true)
                return await Task.FromResult<StructureClaims?>(null);

            return await Task.FromResult(
                new StructureClaims(
                Guid.Parse(claims.Identity.FindFirstValue(ClaimTypes.NameIdentifier)),
                claims.Identity.FindFirstValue(ClaimTypes.Name), 
                claims.Identity.FindFirstValue(ClaimTypes.SerialNumber)));
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }

    public static async Task<(Guid Id, IdentityCurrentType Type)> GetCurrentStructureIdAsync(this ClaimsPrincipal claims)
    {
        try
        {
            if (claims is null || claims.Identity is null || !claims.Identity.IsAuthenticated)
                return await Task.FromResult((Guid.Empty, IdentityCurrentType.NotAuthenticated));

            if (claims.Identity.FindFirstValue(ClaimTypes.SerialNumber) == null)
                return await Task.FromResult((Guid.Empty, IdentityCurrentType.User));

            return await Task.FromResult((Guid.Parse(claims.Identity.FindFirstValue(ClaimTypes.NameIdentifier)), IdentityCurrentType.Structure));
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
    #endregion
}
