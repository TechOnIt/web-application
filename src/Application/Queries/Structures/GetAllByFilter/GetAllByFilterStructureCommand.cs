using TechOnIt.Application.Common.Models.ViewModels.Structures;
using TechOnIt.Application.Reports.StructuresAggregate;
using Microsoft.AspNetCore.Mvc;

namespace TechOnIt.Application.Queries.Structures.GetAllByFilter;

public class GetAllByFilterStructureCommand : IRequest<PaginatedList<StructureViewModel>>
{
    // TODO:
    // This is a crime against humanity!                                            WHY?
    public Expression<Func<Structure, bool>>? Filter { get; set; }
}

public class GetAllByFilterStructureCommandHandler : IRequestHandler<GetAllByFilterStructureCommand, Result<IList<StructureViewModel>>>
{
    #region Ctor
    private readonly IUnitOfWorks _unitOfWorks;
    public GetAllByFilterStructureCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion

    public async Task<Result<IList<StructureViewModel>>> Handle(GetAllByFilterStructureCommand request, CancellationToken cancellationToken = default)
    {
        var allStructures = await _structureAggregateReports.GetStructuresByFilterAsync(request.Filter, cancellationToken);
        return Result.Ok(allStructures);
    }
}