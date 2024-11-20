using TechOnIt.Application.Common.Interfaces.Repositories;

namespace TechOnIt.Application.Common.Interfaces;

public interface IUnitOfWorks
{
    Task SaveAsync(CancellationToken stoppingToken = default, bool fixArabicChars = false);
    IAppDbContext _context { get; }

    IStructureRepository StructureRepository { get; }
    IUserRepository UserRepository { get; }
    IRoleRepository RoleRepository { get; }
    ISensorRepository SensorRepository { get; }
    IRelayRepositry RelayRepositry { get; }
    IReportRepository ReportRepository { get; }
    IAdoRepository AdoRepository { get; }
    IDynamicAccessRepository DynamicAccessRepository { get; }
}