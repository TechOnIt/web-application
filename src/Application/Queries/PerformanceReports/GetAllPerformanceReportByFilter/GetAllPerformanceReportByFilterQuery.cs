using iot.Application.Common.Models.ViewModels.Reports;
using iot.Domain.Entities.Product.SensorAggregate;
using iot.Infrastructure.Repositories.UnitOfWorks;
using Mapster;
using System.Linq.Expressions;

namespace iot.Application.Queries.PerformanceReports.GetAllPerformanceReportByFilter;

public class GetAllPerformanceReportByFilterQuery : IRequest<Result<IList<PerformanceReportViewModel>>>
{
    public Expression<Func<PerformanceReport, bool>>? Filter { get; set; }
    public Func<IQueryable, IOrderedQueryable<PerformanceReport>>? Ordered { get; set; }
    public Expression<Func<PerformanceReport, object>>[]? IncludesTo { get; set; }
}

public class GetAllPerformanceReportByFilterQueryHandler : IRequestHandler<GetAllPerformanceReportByFilterQuery, Result<IList<PerformanceReportViewModel>>>
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public GetAllPerformanceReportByFilterQueryHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion

    public async Task<Result<IList<PerformanceReportViewModel>>> Handle(GetAllPerformanceReportByFilterQuery request, CancellationToken cancellationToken = default)
    {
        var allReports = _unitOfWorks.SqlRepository<PerformanceReport>().TableNoTracking.AsQueryable();
        if (allReports.Any())
        {
            if (request.IncludesTo != null)
            {
                foreach (var item in request.IncludesTo)
                {
                    allReports = allReports.Include(item);
                }
            }

            if (request.Filter != null)
            {
                allReports = allReports.Where(request.Filter);
            }

            if (request.Ordered != null)
            {
                allReports = request.Ordered(allReports);
            }
        }

        var exutedQuery = await allReports.ToListAsync(cancellationToken);
        return Result.Ok(exutedQuery.Adapt<IList<PerformanceReportViewModel>>());
    }
}