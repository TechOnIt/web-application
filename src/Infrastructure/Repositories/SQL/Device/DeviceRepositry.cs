using iot.Infrastructure.Persistence.Context.Identity;

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

    public async Task<Domain.Entities.Product.Device> CreateDeviceAsync(Domain.Entities.Product.Device device, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Domain.Entities.Product.Device> DeleteDeviceByIdAsync(Guid DeviceId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Domain.Entities.Product.Device> FindDeviceByIdAsync(Guid deviceId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Domain.Entities.Product.Device> FindDeviceByIdAsyncAsNoTracking(Guid deviceId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Domain.Entities.Product.Device> UpdateDeviceAsync(Domain.Entities.Product.Device device, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
