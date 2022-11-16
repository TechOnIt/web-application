using iot.Infrastructure.Persistence.Context;
using iot.Infrastructure.Repositories.SQL;
using iot.Infrastructure.Repositories.SQL.Device;
using iot.Infrastructure.Repositories.SQL.Roles;
using iot.Infrastructure.Repositories.SQL.SensorAggregate;
using iot.Infrastructure.Repositories.SQL.StructureAggregateRepository;
using iot.Infrastructure.Repositories.SQL.Users;

namespace iot.Infrastructure.Repositories.UnitOfWorks;

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