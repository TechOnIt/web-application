using TechOnIt.Application.Common.Models.ViewModels.Devices;

namespace TechOnIt.Application.Services.ProductServices;

public class DeviceService : IDeviceService
{
    #region Ctor
    private readonly IUnitOfWorks _unitOfWorks; // _unitOfWork is better than _uow : _uow is not clean !
    public DeviceService(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<DeviceViewModel?> FindByIdAsync(Guid deviceId, CancellationToken cancellationToken = default)
    {
        var getDevice = await _unitOfWorks.DeviceRepositry.FindByIdAsync(deviceId, cancellationToken);
        if (getDevice is null)
            return await Task.FromResult<DeviceViewModel?>(null);

        return await Task.FromResult<DeviceViewModel?>(getDevice.Adapt<DeviceViewModel>());
    }

    public async Task<DeviceViewModel?> FindByIdAsNoTrackingAsync(Guid deviceId, CancellationToken cancellationToken = default)
    {
        var getDevice = await _unitOfWorks.DeviceRepositry.FindByIdAsNoTrackingAsync(deviceId, cancellationToken);
        if (getDevice is null)
            return await Task.FromResult<DeviceViewModel?>(null);

        return await Task.FromResult<DeviceViewModel?>(getDevice.Adapt<DeviceViewModel>());
    }

    public async Task<DeviceViewModel?> CreateAsync(DeviceViewModel device, CancellationToken cancellationToken = default)
    {
        device.Id = Guid.NewGuid();
        await _unitOfWorks.DeviceRepositry.CreateAsync(device.Adapt<Device>(), cancellationToken);
        return await Task.FromResult<DeviceViewModel?>(device);
    }

    public async Task<DeviceViewModel?> UpdateAsync(DeviceViewModel device, CancellationToken cancellationToken = default)
    {
        var getrecentDevice = await _unitOfWorks.DeviceRepositry.FindByIdAsync(device.Id, cancellationToken);
        if (getrecentDevice is null)
            return await Task.FromResult<DeviceViewModel?>(null);

        getrecentDevice.Pin = device.Pin;
        getrecentDevice.IsHigh = device.IsHigh;
        getrecentDevice.SetDeviceType(device.DeviceType);

        await _unitOfWorks.DeviceRepositry.UpdateAsync(getrecentDevice, cancellationToken);
        return await Task.FromResult(device);
    }

    public async Task<bool?> DeleteByIdAsync(Guid DeviceId, CancellationToken cancellationToken = default)
    {
        var device = await _unitOfWorks.DeviceRepositry.FindByIdAsync(DeviceId,cancellationToken);
        if(device is null) return await Task.FromResult<bool?>(null);
        else
        {
            await _unitOfWorks.DeviceRepositry.DeleteAsync(device,cancellationToken);
        }

        return await Task.FromResult(true);
    }
}