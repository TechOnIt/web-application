using TechOnIt.Application.Common.Models.ViewModels.Users.Authentication;
using TechOnIt.Domain.Entities.Identity.UserAggregate;

namespace TechOnIt.Application.Common.Security.JwtBearer;

public interface IJwtService
{
    Task<AccessToken> GenerateAccessToken(User user, IList<Role> userRoles, CancellationToken cancellationToken);
}