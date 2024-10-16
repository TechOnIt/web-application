using TechOnIt.Application.Common.Models.ViewModels.Structures;
using TechOnIt.Application.Reports.StructuresAggregate;

namespace TechOnIt.Application.Commands.Structures.Dashboard.GetMyStructures;

public class GetMyStructuresCardCommand : IRequest<IList<StructureCardViewModel>>
{
    public Guid UserId { get; set; }
}

public class GetMyStructuresCardCommandHandler : IRequestHandler<GetMyStructuresCardCommand, IList<StructureCardViewModel>>
{
    #region Ctor
    private readonly StructureAggregateReports _structureReports;

    public GetMyStructuresCardCommandHandler(StructureAggregateReports structureReports)
    {
        _structureReports = structureReports;
    }
    #endregion
    public async Task<IList<StructureCardViewModel>> Handle(GetMyStructuresCardCommand request, CancellationToken cancellationToken)
        => await _structureReports.GetStructureCardByUserIdNoTrackAsync(request.UserId, cancellationToken);
}