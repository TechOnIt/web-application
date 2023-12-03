using TechOnIt.Infrastructure.Persistence.Context;
using TechOnIt.Infrastructure.Repositories.SQL.Devices;
using TechOnIt.Infrastructure.Repositories.SQL.DynamicAccess;
using TechOnIt.Infrastructure.Repositories.SQL.HeavyTransaction;
using TechOnIt.Infrastructure.Repositories.SQL.Reports;
using TechOnIt.Infrastructure.Repositories.SQL.Roles;
using TechOnIt.Infrastructure.Repositories.SQL.SensorAggregate;
using TechOnIt.Infrastructure.Repositories.SQL.StructureAggregateRepository;
using TechOnIt.Infrastructure.Repositories.SQL.Users;

namespace TechOnIt.Infrastructure.Repositories.UnitOfWorks;

public interface IUnitOfWorks
{
    Task SaveAsync(CancellationToken stoppingToken = default, bool fixArabicChars = false);
    IdentityContext _context { get; }

    IStructureRepository StructureRepository { get; }
    IUserRepository UserRepository { get; }
    IRoleRepository RoleRepository { get; }
    ISensorRepository SensorRepository { get; }
    IDeviceRepositry DeviceRepositry { get; }
    IReportRepository ReportRepository { get; }
    IAdoRepository AdoRepository { get; }
    IDynamicAccessRepository DynamicAccessRepository { get; }
}