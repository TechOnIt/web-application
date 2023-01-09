using TechOnIt.Application.Common.Models.ViewModels.Reports;

namespace TechOnIt.Application.Queries.PerformanceReports.GetAllPerformanceReportByFilter;

public class GetAllPerformanceReportByFilterQuery : IRequest<object>
{
    public Guid SensorId { get; set; }
}

public class GetAllPerformanceReportByFilterQueryHandler : IRequestHandler<GetAllPerformanceReportByFilterQuery, object>
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public GetAllPerformanceReportByFilterQueryHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion

    public async Task<object> Handle(GetAllPerformanceReportByFilterQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var allReports = _unitOfWorks.SensorRepository.GetSensorReportBySensorIdAsNoTrackingAsync(request.SensorId,cancellationToken);
            if (allReports is null)
                return await Task.FromResult(ResultExtention.NotFound($"can not found reports for sensor with id : {request.SensorId}"));

            return await Task.FromResult(ResultExtention.ListResult(allReports.Adapt<IList<PerformanceReportViewModel>>()));
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}