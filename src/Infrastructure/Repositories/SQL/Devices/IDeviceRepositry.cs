using TechOnIt.Domain.Entities.Product;

namespace TechOnIt.Infrastructure.Repositories.SQL.Devices;

public interface IDeviceRepositry
{
    Task<Device?> FindByIdAsync(Guid deviceId, CancellationToken cancellationToken = default);
    Task<Device?> FindByIdAsNoTrackingAsync(Guid deviceId, CancellationToken cancellationToken = default);
    Task CreateAsync(Device device, CancellationToken cancellationToken = default);
    Task UpdateAsync(Device device, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(Guid DeviceId, CancellationToken cancellationToken = default);
    Task DeleteAsync(Device device, CancellationToken cancellationToken);
}
