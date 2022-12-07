using TechOnIt.Infrastructure.Persistence.Context;
using TechOnIt.Infrastructure.Repositories.SQL;
using TechOnIt.Infrastructure.Repositories.SQL.Devices;
using TechOnIt.Infrastructure.Repositories.SQL.Roles;
using TechOnIt.Infrastructure.Repositories.SQL.SensorAggregate;
using TechOnIt.Infrastructure.Repositories.SQL.StructureAggregateRepository;
using TechOnIt.Infrastructure.Repositories.SQL.Users;

namespace TechOnIt.Infrastructure.Repositories.UnitOfWorks;

public interface IUnitOfWorks
{
    Task SaveAsync(CancellationToken stoppingToken = default, bool fixArabicChars = false);
    ISqlRepository<TEntity> SqlRepository<TEntity>() where TEntity : class;
    IdentityContext _context { get; }

    IStructureRepository StructureRepository { get; }
    IUserRepository UserRepository { get; }
    IRoleRepository RoleRepository { get; }
    ISensorRepository SensorRepository { get; }
    IDeviceRepositry DeviceRepositry { get; }
}