namespace TechOnIt.Application.Reports.Devices;

public interface IDeviceReport : IReport
{
    Task<List<TDestination>> GetDevicesByStructureIdNoTrackAsync<TDestination>(Guid structureId,
        TypeAdapterConfig config = null, CancellationToken cancellationToken = default);
}