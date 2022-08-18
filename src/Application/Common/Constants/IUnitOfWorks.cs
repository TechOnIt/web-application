using iot.Application.Repositories.SQL;
using iot.Application.Repositories.SQL.Users;
using iot.Infrastructure.Persistence.Context;

namespace iot.Application.Common.Constants
{
    public interface IUnitOfWorks
    {
        ISqlRepository<TEntity> SqlRepository<TEntity>() where TEntity : class;
        IUserRepository UserRepository { get; }
        IIdentityContext _context { get; }
        Task SaveAsync();
    }
}
