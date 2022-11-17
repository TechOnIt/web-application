using iot.Application.Common.ViewModels;

namespace iot.Application.Services.ProductServices.ProductContracts;

public interface IDeviceService
{
    /// <summary>
    /// Create a new device.
    /// </summary>
    /// <param name="device">Device model object.</param>
    Task<DeviceViewModel?> CreateDeviceAsync(DeviceViewModel device, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an specific device.
    /// </summary>
    /// <param name="device">Device model object.</param>
    Task<DeviceViewModel?> UpdateDeviceAsync(DeviceViewModel device, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete an specific device.
    /// </summary>
    /// <param name="DeviceId">Device unique id for delete.</param>
    Task<bool> DeleteDeviceByIdAsync(Guid DeviceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Find an specific device by id.
    /// </summary>
    /// <param name="deviceId">Device unique id to find.</param>
    Task<DeviceViewModel?> FindDeviceByIdAsync(Guid deviceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Find an specific device by id (As no tracking).
    /// </summary>
    /// <param name="deviceId">Device unique id to find.</param>
    Task<DeviceViewModel?> FindDeviceByIdAsyncAsNoTracking(Guid deviceId, CancellationToken cancellationToken = default);
}