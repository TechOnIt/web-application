using iot.Domain.Entities.Identity;
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

    Task<User?> FindUserByPhoneNumberWithRolesAsyncNoTracking(string phonenumber, CancellationToken cancellationToken);
    Task<User> CreateNewUser(User user, CancellationToken cancellationToken);
    Task<bool> IsExistsUserByPhoneNumberAsync(string phonenumber);
    Task<string> GetUserEmailByPhoneNumberAsync(string phonenumber);
    Task<User?> FindUserByUserIdAsNoTrackingAsync(Guid userId);
    Task<User?> FindUserByUserIdAsync(Guid userId);
    Task DeleteUserByPhoneNumberAsync(string phonenumber, CancellationToken cancellationToken);
    Task DeleteUserByIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<bool> IsExistsUserByIdAsync(Guid userId,CancellationToken cancellationToken);
    Task UpdateUserAsync(User user, CancellationToken cancellationToken);
}