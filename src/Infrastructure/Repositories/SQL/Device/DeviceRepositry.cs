using iot.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace iot.Infrastructure.Repositories.SQL.Device;

public class DeviceRepositry : IDeviceRepositry
{
    #region constructor
    private readonly IdentityContext _context;

    public DeviceRepositry(IdentityContext context)
    {
        _context = context;
    }

    #endregion

    public async Task CreateDeviceAsync(Domain.Entities.Product.Device device, CancellationToken cancellationToken = default)
    {
        var result = await _context.Devices.AddAsync(device, cancellationToken);
        await Task.CompletedTask;
    }

    public async Task DeleteDeviceByIdAsync(Guid DeviceId, CancellationToken cancellationToken = default)
    {
        var getDevice = await _context.Devices.FirstOrDefaultAsync(a=>a.Id==DeviceId);

        if (getDevice is null)
            await Task.CompletedTask;

        if(!cancellationToken.IsCancellationRequested)
            _context.Entry<Domain.Entities.Product.Device>(getDevice).State = EntityState.Deleted;

        await Task.CompletedTask;
    }

    public async Task UpdateDeviceAsync(Domain.Entities.Product.Device device, CancellationToken cancellationToken = default)
    {
        if (!cancellationToken.IsCancellationRequested)
            _context.Entry<Domain.Entities.Product.Device>(device).State = EntityState.Modified;

        await Task.CompletedTask;
    }

    public async Task<Domain.Entities.Product.Device?> FindDeviceByIdAsync(Guid deviceId, CancellationToken cancellationToken = default)
    {
        var getDevice = await _context.Devices.FirstOrDefaultAsync(a=>a.Id==deviceId,cancellationToken);
        if (getDevice is null)
        {
            await Task.CompletedTask;
            return await Task.FromResult<Domain.Entities.Product.Device?>(null);
        }

        return await Task.FromResult(getDevice);
    }

    public async Task<Domain.Entities.Product.Device?> FindDeviceByIdAsyncAsNoTracking(Guid deviceId, CancellationToken cancellationToken = default)
    {
        var getDevice = await _context.Devices
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == deviceId, cancellationToken);

        if (getDevice is null)
            return await Task.FromResult<Domain.Entities.Product.Device?>(null);

        return await Task.FromResult<Domain.Entities.Product.Device?>(getDevice);
    }
}
