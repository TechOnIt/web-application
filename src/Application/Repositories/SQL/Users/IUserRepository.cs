using iot.Application.Common.DTOs.Users.Authentication;
using System.Linq.Expressions;

namespace iot.Application.Repositories.SQL.Users;

public interface IUserRepository
{
    Task<User?> FindByUsernameAsync(string username, CancellationToken cancellationToken = default);

    /// <summary>
    /// Find user by email or phone number with roles (AsNoTracking).
    /// </summary>
    /// <param name="identity">Email or Phone number</param>
    /// <returns>An specific user.</returns>
    Task<User?> FindByIdentityWithRolesAsync(string identity, CancellationToken stoppingToken = default);
    Task<IList<User>?> GetAllUsersByFilterAsync(Expression<Func<User, bool>>? filter = null,
        CancellationToken cancellationToken = default);


    Task<(string Message, AccessToken Token)> UserSignInByPasswordAsync(string phoneNumber, string password, CancellationToken cancellationToken = default);

    /// <summary>
    /// Generate access token for authorize.
    /// </summary>
    /// <param name="user">User instance with roles.</param>
    /// <returns>Access token & refresh token with expiration date time.</returns>
    Task<AccessToken> GenerateAccessToken(User user, CancellationToken stoppingToken = default);
}