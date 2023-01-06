using System.Security.Claims;
using TechOnIt.Application.Common.Models.ViewModels.Users.Authentication;

namespace TechOnIt.Application.Common.Security.JwtBearer;

public interface IJwtService
{
    Task<string> GenerateTokenAsync(IEnumerable<Claim> claims, DateTime expiresAt, CancellationToken cancellationToken);
}