using TechOnIt.Domain.Entities.Identity.UserAggregate;
using System.Security.Claims;
using TechOnIt.Application.Common.Models.ViewModels.Users.Authentication;

namespace TechOnIt.Application.Common.Security.JwtBearer;

public interface IJwtService
{
    string GenerateTokenWithClaims(List<Claim> claims, DateTime? expireDateTime = null);
    Task<AccessToken> GenerateAccessToken(User user, CancellationToken stoppingToken = default);
}