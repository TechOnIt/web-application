using iot.Application.Common.Models.ViewModels.Structures;
using iot.Application.Reports.StructuresAggregate;
using iot.Domain.Entities.Product.StructureAggregate;
using System.Linq.Expressions;


namespace iot.Application.Queries.Structures.GetAllByFilter;

public class GetAllByFilterStructureCommand : IRequest<Result<IList<StructureViewModel>>>
{
    // TODO:
    // This is a crime against humanity!                                            WHY?
    public Expression<Func<Structure, bool>>? Filter { get; set; }
}

public class GetAllByFilterStructureCommandHandler : IRequestHandler<GetAllByFilterStructureCommand, Result<IList<StructureViewModel>>>
{
    #region Ctor
    private readonly IStructureAggregateReports _structureAggregateReports;

    public GetAllByFilterStructureCommandHandler(IStructureAggregateReports structureAggregateReports)
    {
        _structureAggregateReports = structureAggregateReports;
    }

    #endregion

    public async Task<Result<IList<StructureViewModel>>> Handle(GetAllByFilterStructureCommand request, CancellationToken cancellationToken = default)
    {
        var allStructures = await _structureAggregateReports.GetStructuresByFilterAsync(request.Filter, cancellationToken);
        return Result.Ok(allStructures);
    }
}