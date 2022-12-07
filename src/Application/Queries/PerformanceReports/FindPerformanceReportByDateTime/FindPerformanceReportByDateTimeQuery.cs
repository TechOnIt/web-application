using TechOnIt.Domain.Entities.Product.SensorAggregate;
using TechOnIt.Infrastructure.Repositories.UnitOfWorks;
using Mapster;
using TechOnIt.Application.Common.Models.ViewModels.Reports;

namespace TechOnIt.Application.Queries.PerformanceReports.FindPerformanceReportByDateTime;

public class FindPerformanceReportByDateTimeQuery : IRequest<Result<IList<PerformanceReportViewModel>>>
{
    public DateTime MinDate { get; set; }
    public DateTime? MaxDate { get; set; }
}

public class FindPerformanceReportByDateTimeQueryHandler : IRequestHandler<FindPerformanceReportByDateTimeQuery, Result<IList<PerformanceReportViewModel>>>
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;

    public FindPerformanceReportByDateTimeQueryHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<Result<IList<PerformanceReportViewModel>>> Handle(FindPerformanceReportByDateTimeQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            if (request.MaxDate == null)
                request.MaxDate = DateTime.Now;

            var getReports = _unitOfWorks.SqlRepository<PerformanceReport>()
                .TableNoTracking
                .AsParallel()
                .Where(a => a.RecordDateTime > request.MinDate && a.RecordDateTime < request.MaxDate).ToList();

            if (getReports is null || getReports.Count() == 0)
                return Result.Fail($"there was no repoprt between {request.MinDate.ToShortDateString()} and {request.MaxDate}");

            var model = getReports.Adapt<IList<PerformanceReportViewModel>>();
            return Result.Ok(model);
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}