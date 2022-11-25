namespace iot.Infrastructure.Repositories.SQL.Device;

public interface IDeviceRepositry
{
    Task CreateDeviceAsync(Domain.Entities.Product.Device device,CancellationToken cancellationToken = default);
    Task UpdateDeviceAsync(Domain.Entities.Product.Device device,CancellationToken cancellationToken = default);
    Task DeleteDeviceByIdAsync(Guid DeviceId,CancellationToken cancellationToken = default);
    Task<Domain.Entities.Product.Device> FindDeviceByIdAsync(Guid deviceId,CancellationToken cancellationToken = default);
    Task<Domain.Entities.Product.Device> FindDeviceByIdAsyncAsNoTracking(Guid deviceId,CancellationToken cancellationToken = default);
}
