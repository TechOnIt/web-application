using TechOnIt.Application.Common.Models.ViewModels.Reports;

namespace TechOnIt.Application.Queries.SensorReports.GetAllSensorReportByFilter;

public class GetAllSensorReportByFilterQuery : IRequest<object>
{
    public Guid SensorId { get; set; }
}

public class GetAllSensorReportByFilterQueryHandler : IRequestHandler<GetAllSensorReportByFilterQuery, object>
{
    private readonly IUnitOfWorks _unitOfWorks;
    public GetAllSensorReportByFilterQueryHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    public async Task<object> Handle(GetAllSensorReportByFilterQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var allReports = _unitOfWorks.SensorRepository.GetSensorReportBySensorIdAsNoTrackingAsync(request.SensorId,cancellationToken);
            if (allReports is null)
                return await Task.FromResult(ResultExtention.NotFound($"can not found reports for sensor with id : {request.SensorId}"));

            return await Task.FromResult(ResultExtention.ListResult(allReports.Adapt<IList<SensorReportViewModel>>()));
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}