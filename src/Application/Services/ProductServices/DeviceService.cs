namespace iot.Application.Services.ProductServices;

public class DeviceService : IDeviceService
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public DeviceService(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<DeviceViewModel?> CreateDeviceAsync(DeviceViewModel device, CancellationToken cancellationToken = default)
    {
        try
        {
            device.Id = Guid.NewGuid();
            await _unitOfWorks.DeviceRepositry.CreateDeviceAsync(device.Adapt<Device>(), cancellationToken);
            return await Task.FromResult<DeviceViewModel?>(device);
        }
        catch
        {
            return await Task.FromResult<DeviceViewModel?>(null);
        }
    }

    public async Task<bool> DeleteDeviceByIdAsync(Guid DeviceId, CancellationToken cancellationToken = default)
    {
        await _unitOfWorks.DeviceRepositry.DeleteDeviceByIdAsync(DeviceId, cancellationToken);
        return await Task.FromResult(true);
    }

    public async Task<DeviceViewModel?> UpdateDeviceAsync(DeviceViewModel device, CancellationToken cancellationToken = default)
    {
        var getrecentDevice = await _unitOfWorks.DeviceRepositry.FindDeviceByIdAsync(device.Id,cancellationToken);
        if (getrecentDevice is null)
            return await Task.FromResult<DeviceViewModel?>(null);

        getrecentDevice.Pin = device.Pin;
        getrecentDevice.IsHigh = device.IsHigh;
        getrecentDevice.SetDeviceType(device.DeviceType);

        await _unitOfWorks.DeviceRepositry.UpdateDeviceAsync(getrecentDevice, cancellationToken);
        return await Task.FromResult(device);
    }

    public async Task<DeviceViewModel?> FindDeviceByIdAsync(Guid deviceId, CancellationToken cancellationToken = default)
    {
        var getDevice = await _unitOfWorks.DeviceRepositry.FindDeviceByIdAsync(deviceId,cancellationToken);
        if (getDevice is null)
            return await Task.FromResult<DeviceViewModel?>(null);

       return await Task.FromResult<DeviceViewModel?>(getDevice.Adapt<DeviceViewModel>());
    }

    public async Task<DeviceViewModel?> FindDeviceByIdAsyncAsNoTracking(Guid deviceId, CancellationToken cancellationToken = default)
    {
        var getDevice = await _unitOfWorks.DeviceRepositry.FindDeviceByIdAsyncAsNoTracking(deviceId, cancellationToken);
        if (getDevice is null)
            return await Task.FromResult<DeviceViewModel?>(null);

        return await Task.FromResult<DeviceViewModel?>(getDevice.Adapt<DeviceViewModel>());
    }
}