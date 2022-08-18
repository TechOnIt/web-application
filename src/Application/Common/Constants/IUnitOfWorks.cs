using iot.Application.Repositories.SQL;
using iot.Application.Repositories.SQL.Roles;
using iot.Application.Repositories.SQL.Users;
using iot.Infrastructure.Persistence.Context.Identity;

namespace iot.Application.Common.Constants;

public interface IUnitOfWorks
{
    Task SaveAsync(CancellationToken stoppingToken = default);
    ISqlRepository<TEntity> SqlRepository<TEntity>() where TEntity : class;
    IIdentityContext _context { get; }

    IUserRepository UserRepository { get; }
    IRoleRepository RoleRepository { get; }
}