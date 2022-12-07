using iot.Application.Common.Models.ViewModels.Devices;

namespace iot.Application.Services.ProductServices.ProductContracts;

public interface IDeviceService
{
    /// <summary>
    /// Find an specific device by id.
    /// </summary>
    /// <param name="deviceId">Device unique id to find.</param>
    Task<DeviceViewModel?> FindByIdAsync(Guid deviceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Find an specific device by id (As no tracking).
    /// </summary>
    /// <param name="deviceId">Device unique id to find.</param>
    Task<DeviceViewModel?> FindByIdAsNoTrackingAsync(Guid deviceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new device.
    /// </summary>
    /// <param name="device">Device model object.</param>
    Task<DeviceViewModel?> CreateAsync(DeviceViewModel device, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an specific device.
    /// </summary>
    /// <param name="device">Device model object.</param>
    Task<DeviceViewModel?> UpdateAsync(DeviceViewModel device, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete an specific device.
    /// </summary>
    /// <param name="DeviceId">Device unique id for delete.</param>
    Task<bool> DeleteByIdAsync(Guid DeviceId, CancellationToken cancellationToken = default);
}