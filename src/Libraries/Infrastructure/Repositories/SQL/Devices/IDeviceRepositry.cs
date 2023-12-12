using TechOnIt.Domain.Entities;

namespace TechOnIt.Infrastructure.Repositories.SQL.Devices;

public interface IDeviceRepositry
{
    Task<RelayEntity?> FindByIdAsync(Guid deviceId, CancellationToken cancellationToken = default);
    Task<RelayEntity?> FindByIdAsNoTrackingAsync(Guid deviceId, CancellationToken cancellationToken = default);
    Task CreateAsync(RelayEntity device, CancellationToken cancellationToken = default);
    Task UpdateAsync(RelayEntity device, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(Guid DeviceId, CancellationToken cancellationToken = default);
    Task DeleteAsync(RelayEntity device, CancellationToken cancellationToken);
    Task<IList<RelayEntity>> GetAllDevicesAsync(CancellationToken cancellationToken, Func<RelayEntity, bool>? filter = null);
}
