using TechOnIt.Domain.Entities.Product.StructureAggregate;
using System.Linq.Expressions;
using TechOnIt.Application.Common.Models.ViewModels.Structures;
using TechOnIt.Application.Reports.StructuresAggregate;
using Microsoft.AspNetCore.Mvc;

namespace TechOnIt.Application.Queries.Structures.GetAllByFilter;

public class GetAllByFilterStructureCommand : IRequest<PaginatedList<StructureViewModel>>
{
    public string Keyword { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}

public class GetAllByFilterStructureCommandHandler : IRequestHandler<GetAllByFilterStructureCommand, PaginatedList<StructureViewModel>>
{
    #region Ctor
    private readonly IStructureAggregateReports _structureAggregateReports;

    public GetAllByFilterStructureCommandHandler(IStructureAggregateReports structureAggregateReports)
    {
        _structureAggregateReports = structureAggregateReports;
    }

    #endregion

    public async Task<PaginatedList<StructureViewModel>> Handle(GetAllByFilterStructureCommand request, CancellationToken cancellationToken = default)
    {
        return await _structureAggregateReports
            .GetAllPaginatedSearchAsync<StructureViewModel>(new PaginatedSearchWithSize()
            {
                Keyword = request.Keyword,
                Page = request.Page,
                PageSize = request.PageSize,
            }
            , config: null, cancellationToken);
    }
}