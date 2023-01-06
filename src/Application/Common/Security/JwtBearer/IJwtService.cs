using System.Security.Claims;
using TechOnIt.Application.Common.Models.ViewModels.Users.Authentication;

namespace TechOnIt.Application.Common.Security.JwtBearer;

public interface IJwtService
{
    Task<AccessToken> GenerateAccessToken(IEnumerable<Claim> claims, CancellationToken cancellationToken);
}