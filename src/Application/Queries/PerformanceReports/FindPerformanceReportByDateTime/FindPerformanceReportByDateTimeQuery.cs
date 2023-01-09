using TechOnIt.Application.Common.Models.ViewModels.Reports;

namespace TechOnIt.Application.Queries.PerformanceReports.FindPerformanceReportByDateTime;

public class FindPerformanceReportByDateTimeQuery : IRequest<object>
{
    public Guid SensorId { get; set; }
    public DateTime MinDate { get; set; }

    private DateTime? _maxDate;
    public DateTime? MaxDate 
    {
        get
        {
            if (_maxDate == null)
            {
                _maxDate = DateTime.Now;
            }

            return _maxDate;
        }
        set
        {
            _maxDate = value;
        }
    }
}

public class FindPerformanceReportByDateTimeQueryHandler : IRequestHandler<FindPerformanceReportByDateTimeQuery, object>
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;

    public FindPerformanceReportByDateTimeQueryHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<object> Handle(FindPerformanceReportByDateTimeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var getReports = 
                await _unitOfWorks
                .SensorRepository
                .GetSensorReportBySensorIdAsNoTrackingWithTimeFilterAsync(request.SensorId,request.MinDate,(DateTime)request.MaxDate,cancellationToken);

            if (getReports is null)
                return await Task.FromResult(ResultExtention.NotFound($"can not found any report in this time and with this id : {request.SensorId}"));

            return getReports.Adapt<IList<PerformanceReportViewModel>>();
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}