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
    Task<User?> FindByPhoneNumberWithRolesNoTrackingAsync(string phonenumber, CancellationToken cancellationToken = default);
    Task<User?> FindByIdAsNoTrackingAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<User?> FindByIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<IList<User>?> GetAllByFilterAsync(Expression<Func<User, bool>>? filter = null,
        CancellationToken cancellationToken = default);
    Task<string> GetEmailByPhoneNumberAsync(string phonenumber, CancellationToken cancellationToken = default);

    Task CreateAsync(User user, CancellationToken cancellationToken = default);

    Task DeleteByPhoneNumberAsync(string phonenumber, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task UpdateAsync(User user, CancellationToken cancellationToken = default);

    Task<bool> IsExistsByQueryAsync(Expression<Func<User, bool>> query, CancellationToken cancellationToken = default);
    Task<bool> IsExistsByPhoneNumberAsync(string phonenumber, CancellationToken cancellationToken = default);
    Task<bool> IsExistsByIdAsync(Guid userId,CancellationToken cancellationToken = default);
}