using iot.Domain.Entities.Identity;

namespace iot.Application.Repositories.SQL.Users;

public interface IUserRepository : ISqlRepository<User>
{
    Task<User?> FindByUsernameAsync(string username, CancellationToken cancellationToken = default);
}