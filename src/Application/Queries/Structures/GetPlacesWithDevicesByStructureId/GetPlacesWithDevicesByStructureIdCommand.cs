using TechOnIt.Application.Common.Models.ViewModels.Structures;
using TechOnIt.Application.Reports.StructuresAggregate;

namespace TechOnIt.Application.Queries.Structures.GetPlacesWithDevicesByStructureId;

public class GetPlacesWithDevicesByStructureIdCommand : IRequest<StructurePlacesWithDevicesViewModel?>
{
    public Guid StructureId { get; set; }
}

public class GetPlacesWithDevicesByStructureIdCommandHandler : IRequestHandler<GetPlacesWithDevicesByStructureIdCommand, StructurePlacesWithDevicesViewModel?>
{
    #region Ctor
    private readonly IStructureAggregateReports _structureReports;

    public GetPlacesWithDevicesByStructureIdCommandHandler(IStructureAggregateReports structureReports)
    {
        _structureReports = structureReports;
    }
    #endregion

    public async Task<StructurePlacesWithDevicesViewModel?> Handle(GetPlacesWithDevicesByStructureIdCommand request, CancellationToken cancellationToken)
        => await _structureReports.GetStructureWithPlacesAndDevicesByIdNoTrackAsync(request.StructureId, cancellationToken);
}