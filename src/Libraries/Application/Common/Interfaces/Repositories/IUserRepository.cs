using System.Linq.Expressions;
using TechOnIt.Domain.Entities.Identities.UserAggregate;

namespace TechOnIt.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    Task<UserEntity> FindByUsernameAsync(string username, CancellationToken cancellationToken = default);

    /// <summary>
    /// Find user by email or phone number with roles (AsNoTracking).
    /// </summary>
    /// <param name="identity">Email or Phone number</param>
    /// <returns>An specific user.</returns>
    Task<UserEntity> FindByIdentityWithRolesAsync(string identity, CancellationToken stoppingToken = default);
    Task<UserEntity> FindByPhoneNumberWithRolesNoTrackingAsync(string phonenumber, CancellationToken cancellationToken = default);

    Task<UserEntity> FindByUsernameWithRolesNoTrackingAsync(string username, CancellationToken cancellationToken = default);
    Task<UserEntity> FindByIdAsNoTrackingAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<UserEntity> FindByIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<IList<UserEntity>> GetAllByFilterAsync(Expression<Func<UserEntity, bool>> filter = null,
        CancellationToken cancellationToken = default);
    Task<string> GetEmailByPhoneNumberAsync(string phonenumber, CancellationToken cancellationToken = default);

    Task CreateAsync(UserEntity user, CancellationToken cancellationToken = default);

    Task DeleteByPhoneNumberAsync(string phonenumber, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task UpdateAsync(UserEntity user, CancellationToken cancellationToken = default);

    Task<bool> IsExistsByQueryAsync(Expression<Func<UserEntity, bool>> query, CancellationToken cancellationToken = default);
    Task<bool> IsExistsByPhoneNumberAsync(string phonenumber, CancellationToken cancellationToken = default);
    Task<bool> IsExistsByIdAsync(Guid userId, CancellationToken cancellationToken = default);
}