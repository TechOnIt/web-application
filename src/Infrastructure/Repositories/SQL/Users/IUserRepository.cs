using iot.Domain.Entities.Identity.UserAggregate;
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

    Task<User?> FindUserByPhoneNumberWithRolesAsyncNoTracking(string phonenumber, CancellationToken cancellationToken = default);
    Task<User> CreateNewUser(User user, CancellationToken cancellationToken = default);
    Task<bool> IsExistsUserByPhoneNumberAsync(string phonenumber, CancellationToken cancellationToken = default);
    Task<string> GetUserEmailByPhoneNumberAsync(string phonenumber, CancellationToken cancellationToken = default);
    Task<User?> FindUserByUserIdAsNoTrackingAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<User?> FindUserByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task DeleteUserByPhoneNumberAsync(string phonenumber, CancellationToken cancellationToken = default);
    Task DeleteUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> IsExistsUserByIdAsync(Guid userId,CancellationToken cancellationToken = default);
    Task UpdateUserAsync(User user, CancellationToken cancellationToken = default);
}