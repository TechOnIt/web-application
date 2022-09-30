using iot.Application.Repositories.SQL;
using iot.Application.Repositories.SQL.Roles;
using iot.Application.Repositories.SQL.StructureAggregateRepository;
using iot.Application.Repositories.SQL.Users;
using iot.Infrastructure.Persistence.Context.Identity;
using iot.Infrastructure.Repositories.SQL.SensorAggregate;

namespace iot.Application.Repositories.UnitOfWorks.Identity;

public interface IUnitOfWorks
{
    Task SaveAsync(CancellationToken stoppingToken = default,bool fixArabicChars = false);
    ISqlRepository<TEntity> SqlRepository<TEntity>() where TEntity : class;
    IdentityContext _context { get; }

    IStructureRepository StructureRepository { get; }
    IUserRepository UserRepository { get; }
    IRoleRepository RoleRepository { get; }
    ISensorRepository SensorRepository { get; }
}