using TechOnIt.Application.Common.Models.ViewModels.Structures;
using TechOnIt.Application.Reports.StructuresAggregate;

namespace TechOnIt.Application.Queries.Structures.GetGroupsWithRelaysByStructureId;

public class GetGroupsWithRelaysByStructureIdCommand : IRequest<StructureGroupsWithRelayViewModel>
{
    public Guid StructureId { get; set; }
}

public class GetGroupsWithRelaysByStructureIdCommandHandler : IRequestHandler<GetGroupsWithRelaysByStructureIdCommand, StructureGroupsWithRelayViewModel>
{
    #region Ctor
    private readonly IStructureAggregateReports _structureReports;

    public GetGroupsWithRelaysByStructureIdCommandHandler(IStructureAggregateReports structureReports)
    {
        _structureReports = structureReports;
    }
    #endregion

    public async Task<StructureGroupsWithRelayViewModel> Handle(GetGroupsWithRelaysByStructureIdCommand request, CancellationToken cancellationToken)
        => await _structureReports.GetStructureWithGroupsAndRelaysByIdNoTrackAsync(request.StructureId, cancellationToken);
}