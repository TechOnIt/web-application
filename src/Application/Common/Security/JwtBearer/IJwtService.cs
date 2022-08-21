using System.Security.Claims;

namespace iot.Application.Common.Security.JwtBearer;

public interface IJwtService
{
    string GenerateTokenWithClaims(List<Claim> claims, DateTime? expireDateTime = null);
}