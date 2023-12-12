using Microsoft.EntityFrameworkCore;
using TechOnIt.Domain.Entities;
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

    public async Task<RelayEntity?> FindByIdAsync(Guid deviceId, CancellationToken cancellationToken = default)
    {
        var getDevice = await _context.Devices.FirstOrDefaultAsync(a => a.Id == deviceId, cancellationToken);
        if (getDevice is null)
        {
            await Task.CompletedTask;
            return await Task.FromResult<RelayEntity?>(null);
        }

        return await Task.FromResult(getDevice);
    }
    public async Task<RelayEntity?> FindByIdAsNoTrackingAsync(Guid deviceId, CancellationToken cancellationToken = default)
    {
        var getDevice = await _context.Devices
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == deviceId, cancellationToken);

        if (getDevice is null)
            return await Task.FromResult<RelayEntity?>(null);

        return await Task.FromResult<RelayEntity?>(getDevice);
    }
    /// <summary>
    /// Create a new device.
    /// </summary>
    /// <param name="device">Device model object.</param>
    public async Task CreateAsync(RelayEntity device, CancellationToken cancellationToken = default)
    {
        var result = await _context.Devices.AddAsync(device, cancellationToken);
        await Task.CompletedTask;
    }
    public async Task UpdateAsync(RelayEntity device, CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
            _context.Devices.Update(device);

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
            _context.Entry<RelayEntity>(getDevice).State = EntityState.Deleted;

        await Task.CompletedTask;
    }
    public async Task DeleteAsync(RelayEntity device, CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            _context.Devices.Remove(device);
        }

        await Task.CompletedTask;
    }

    #region bad method
    public async Task<IList<RelayEntity>> GetAllDevicesAsync(CancellationToken cancellationToken, Func<RelayEntity, bool>? filter = null)
    {
        var devices = _context.Devices.AsNoTracking().AsQueryable();

        if (filter is null)
            return await devices.ToListAsync();

        return await Task.FromResult(devices.Where(filter).ToList());
    }
    #endregion

}