using iot.Domain.Entities.Identity;
using System.Linq.Expressions;

namespace iot.Application.Repositories.SQL.Users;

public interface IUserRepository
{
    Task<User?> FindByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<IList<User>> GetAllUsersByFilterAsync(Expression<Func<User, bool>> filter=null, CancellationToken cancellationToken = default);
}