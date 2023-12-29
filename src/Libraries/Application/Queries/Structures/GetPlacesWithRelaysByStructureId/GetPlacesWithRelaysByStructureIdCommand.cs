using TechOnIt.Application.Common.Models.ViewModels.Structures;
using TechOnIt.Application.Reports.StructuresAggregate;

namespace TechOnIt.Application.Queries.Structures.GetPlacesWithRelaysByStructureId;

public class GetPlacesWithRelaysByStructureIdCommand : IRequest<StructurePlacesWithRelayViewModel>
{
    public Guid StructureId { get; set; }
}

public class GetPlacesWithRelaysByStructureIdCommandHandler : IRequestHandler<GetPlacesWithRelaysByStructureIdCommand, StructurePlacesWithRelayViewModel>
{
    #region Ctor
    private readonly IStructureAggregateReports _structureReports;

    public GetPlacesWithRelaysByStructureIdCommandHandler(IStructureAggregateReports structureReports)
    {
        _structureReports = structureReports;
    }
    #endregion

    public async Task<StructurePlacesWithRelayViewModel> Handle(GetPlacesWithRelaysByStructureIdCommand request, CancellationToken cancellationToken)
        => await _structureReports.GetStructureWithPlacesAndRelaysByIdNoTrackAsync(request.StructureId, cancellationToken);
}