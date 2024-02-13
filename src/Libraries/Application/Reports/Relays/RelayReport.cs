using TechOnIt.Infrastructure.Persistence.Context;

namespace TechOnIt.Application.Reports.Relays;

public class RelayReport : IRelayReport
{
    #region Ctor
    private readonly IdentityContext _context;

    public RelayReport(IdentityContext context)
    {
        _context = context;
    }
    #endregion

    public async Task<List<TDestination>> GetRelaysByStructureIdNoTrackAsync<TDestination>(Guid structureId,
        TypeAdapterConfig config = null, CancellationToken cancellationToken = default)
        => await _context.Relays
            .AsNoTracking()
            .Include(relay => relay.Group)
            .Where(relay => relay.Group.StructureId == structureId)
            .ProjectToType<TDestination>(config)
            .ToListAsync(cancellationToken);
}