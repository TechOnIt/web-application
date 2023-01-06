using System.Security.Claims;
using TechOnIt.Domain.Entities.Identity.UserAggregate;
using TechOnIt.Domain.Entities.Product.StructureAggregate;

namespace TechOnIt.Application.Common.Extentions;

public static class AuthorizationExtentions
{
    public static async Task<IEnumerable<Claim>> GetClaims(this User user, IList<Role> userRoles)
    {
        IList<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,$"{user.FullName.Name} {user.FullName.Surname}"),
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
        };

        if (userRoles.Count() > 0)
        {
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }
        }

        return await Task.FromResult(claims);
    }


    public static async Task<IEnumerable<Claim>> GetClaims(this Structure structure)
    {
        IList<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,$"{structure.Name}"),
            new Claim(ClaimTypes.NameIdentifier,structure.Id.ToString()),
            new Claim(ClaimTypes.SerialNumber,structure.ApiKey.ToString()),
        };

        return await Task.FromResult(claims);
    }
}
