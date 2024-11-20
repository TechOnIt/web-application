namespace TechOnIt.Application.Reports.Relays;

public class RelayReport
{
    #region Ctor
    private readonly IAppDbContext _context;

    public RelayReport(IAppDbContext context)
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