using iot.Application.Common.ViewModels;
using iot.Domain.Entities.Product.SensorAggregate;
using iot.Infrastructure.Repositories.UnitOfWorks;
using Mapster;

namespace iot.Application.Queries.PerformanceReports.FindPerformanceReportById;

public class FindPerformanceReportByIdCommand : IRequest<Result<PerformanceReportViewModel>>
{
    public Guid Id { get; set; }
}

public class FindPerformanceReportByIdCommandHandler : IRequestHandler<FindPerformanceReportByIdCommand, Result<PerformanceReportViewModel>>
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public FindPerformanceReportByIdCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion


    public async Task<Result<PerformanceReportViewModel>> Handle(FindPerformanceReportByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var getReport = await _unitOfWorks.SqlRepository<PerformanceReport>()
                .TableNoTracking.FirstOrDefaultAsync(a => a.Id == request.Id);

            if (getReport is null)
                return Result.Fail($"can not find report with id : {request.Id}");

            return Result.Ok(getReport.Adapt<PerformanceReportViewModel>());
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}