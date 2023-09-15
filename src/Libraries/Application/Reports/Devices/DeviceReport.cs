using TechOnIt.Infrastructure.Persistence.Context;

namespace TechOnIt.Application.Reports.Devices;

public class DeviceReport : IDeviceReport
{
    #region Ctor
    private readonly IdentityContext _context;

    public DeviceReport(IdentityContext context)
    {
        _context = context;
    }
    #endregion

    public async Task<List<TDestination>> GetDevicesByStructureIdNoTrackAsync<TDestination>(Guid structureId,
        TypeAdapterConfig? config = null, CancellationToken cancellationToken = default)
        => await _context.Devices
            .AsNoTracking()
            .Include(device => device.Place)
            .Where(device => device.Place.StructureId == structureId)
            .ProjectToType<TDestination>(config)
            .ToListAsync(cancellationToken);
}