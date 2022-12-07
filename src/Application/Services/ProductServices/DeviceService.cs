using TechOnIt.Application.Common.Models.ViewModels.Devices;
using TechOnIt.Application.Services.ProductServices.ProductContracts;

namespace TechOnIt.Application.Services.ProductServices;

public class DeviceService : IDeviceService
{
    #region Ctor
    private readonly IUnitOfWorks _uow;

    public DeviceService(IUnitOfWorks unitOfWorks)
    {
        _uow = unitOfWorks;
    }
    #endregion
    public async Task<DeviceViewModel?> FindByIdAsync(Guid deviceId, CancellationToken cancellationToken = default)
    {
        var getDevice = await _uow.DeviceRepositry.FindByIdAsync(deviceId, cancellationToken);
        if (getDevice is null)
            return await Task.FromResult<DeviceViewModel?>(null);

        return await Task.FromResult<DeviceViewModel?>(getDevice.Adapt<DeviceViewModel>());
    }

    public async Task<DeviceViewModel?> FindByIdAsNoTrackingAsync(Guid deviceId, CancellationToken cancellationToken = default)
    {
        var getDevice = await _uow.DeviceRepositry.FindByIdAsNoTrackingAsync(deviceId, cancellationToken);
        if (getDevice is null)
            return await Task.FromResult<DeviceViewModel?>(null);

        return await Task.FromResult<DeviceViewModel?>(getDevice.Adapt<DeviceViewModel>());
    }

    public async Task<DeviceViewModel?> CreateAsync(DeviceViewModel device, CancellationToken cancellationToken = default)
    {
        try
        {
            device.Id = Guid.NewGuid();
            await _uow.DeviceRepositry.CreateAsync(device.Adapt<Device>(), cancellationToken);
            return await Task.FromResult<DeviceViewModel?>(device);
        }
        catch
        {
            return await Task.FromResult<DeviceViewModel?>(null);
        }
    }

    public async Task<DeviceViewModel?> UpdateAsync(DeviceViewModel device, CancellationToken cancellationToken = default)
    {
        var getrecentDevice = await _uow.DeviceRepositry.FindByIdAsync(device.Id, cancellationToken);
        if (getrecentDevice is null)
            return await Task.FromResult<DeviceViewModel?>(null);

        getrecentDevice.Pin = device.Pin;
        getrecentDevice.IsHigh = device.IsHigh;
        getrecentDevice.SetDeviceType(device.DeviceType);

        await _uow.DeviceRepositry.UpdateAsync(getrecentDevice, cancellationToken);
        return await Task.FromResult(device);
    }

    public async Task<bool> DeleteByIdAsync(Guid DeviceId, CancellationToken cancellationToken = default)
    {
        await _uow.DeviceRepositry.DeleteByIdAsync(DeviceId, cancellationToken);
        return await Task.FromResult(true);
    }
}