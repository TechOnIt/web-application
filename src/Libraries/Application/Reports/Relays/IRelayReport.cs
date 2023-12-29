namespace TechOnIt.Application.Reports.Relays;

public interface IRelayReport : IReport
{
    Task<List<TDestination>> GetRelaysByStructureIdNoTrackAsync<TDestination>(Guid structureId,
        TypeAdapterConfig config = null, CancellationToken cancellationToken = default);
}