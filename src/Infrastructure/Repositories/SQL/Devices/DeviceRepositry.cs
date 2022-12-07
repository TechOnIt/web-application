using TechOnIt.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using TechOnIt.Infrastructure.Persistence.Context;

namespace TechOnIt.Infrastructure.Repositories.SQL.Devices;

public class DeviceRepositry : IDeviceRepositry
{
    #region Ctor
    private readonly IdentityContext _context;

    public DeviceRepositry(IdentityContext context)
    {
        _context = context;
    }

    #endregion
    public async Task<Device?> FindByIdAsync(Guid deviceId, CancellationToken cancellationToken = default)
    {
        var getDevice = await _context.Devices.FirstOrDefaultAsync(a => a.Id == deviceId, cancellationToken);
        if (getDevice is null)
        {
            await Task.CompletedTask;
            return await Task.FromResult<Domain.Entities.Product.Device?>(null);
        }

        return await Task.FromResult(getDevice);
    }
    public async Task<Device?> FindByIdAsNoTrackingAsync(Guid deviceId, CancellationToken cancellationToken = default)
    {
        var getDevice = await _context.Devices
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == deviceId, cancellationToken);

        if (getDevice is null)
            return await Task.FromResult<Device?>(null);

        return await Task.FromResult<Device?>(getDevice);
    }

    /// <summary>
    /// Create a new device.
    /// </summary>
    /// <param name="device">Device model object.</param>
    public async Task CreateAsync(Device device, CancellationToken cancellationToken = default)
    {
        var result = await _context.Devices.AddAsync(device, cancellationToken);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Device device, CancellationToken cancellationToken = default)
    {
        if (!cancellationToken.IsCancellationRequested)
            _context.Entry<Domain.Entities.Product.Device>(device).State = EntityState.Modified;

        await Task.CompletedTask;
    }

    /// <summary>
    /// Delete an specific device by id.
    /// </summary>
    /// <param name="DeviceId"></param>
    /// <returns></returns>
    public async Task DeleteByIdAsync(Guid DeviceId, CancellationToken cancellationToken = default)
    {
        var getDevice = await _context.Devices.FirstOrDefaultAsync(a => a.Id == DeviceId);

        if (getDevice is null)
            await Task.CompletedTask;

        if (!cancellationToken.IsCancellationRequested)
            _context.Entry<Domain.Entities.Product.Device>(getDevice).State = EntityState.Deleted;

        await Task.CompletedTask;
    }
}