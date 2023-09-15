using System.Security.Claims;
using TechOnIt.Application.Common.Enums.JwtServices;

namespace TechOnIt.Application.Common.Security.JwtBearer;

public interface IJwtService
{
    Task<string> GenerateTokenAsync(IEnumerable<Claim> claims, DateTime expiresAt,
        CancellationToken cancellationToken);

    Task<(IEnumerable<Claim>? Claims, AccessTokenStatus Status)> GetTokenClaimsAsync(string token,
        CancellationToken cancellationToken);
    Task<(Claim? Claim, AccessTokenStatus Status)> FindInTokenClaimsAsync(string token, string ClaimType,
        CancellationToken cancellationToken);
}