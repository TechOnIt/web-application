namespace iot.Infrastructure.Repositories.SQL.Device;

public interface IDeviceRepositry
{
    Task<Domain.Entities.Product.Device> CreateDeviceAsync(Domain.Entities.Product.Device device,CancellationToken cancellationToken);
    Task<Domain.Entities.Product.Device> UpdateDeviceAsync(Domain.Entities.Product.Device device,CancellationToken cancellationToken);
    Task DeleteDeviceByIdAsync(Guid DeviceId,CancellationToken cancellationToken);
    Task<Domain.Entities.Product.Device> FindDeviceByIdAsync(Guid deviceId,CancellationToken cancellationToken);
    Task<Domain.Entities.Product.Device> FindDeviceByIdAsyncAsNoTracking(Guid deviceId,CancellationToken cancellationToken);
}
