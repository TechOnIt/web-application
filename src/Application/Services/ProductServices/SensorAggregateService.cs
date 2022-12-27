using TechOnIt.Application.Common.Models.ViewModels.Reports;
using TechOnIt.Application.Common.Models.ViewModels.Sensors;
using TechOnIt.Domain.Entities.Product.SensorAggregate;

namespace TechOnIt.Application.Services.ProductServices;

public class SensorAggregateService : ISensorAggregateService
{
    #region constructore
    private readonly IUnitOfWorks _unitOfWorks;
    public SensorAggregateService(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion

    public async Task<bool?> CreateNewSensorAsync(SensorViewModel sensor, CancellationToken cancellationToken)
    {
        var model = sensor.Adapt<Sensor>();
        Task create = Task.Factory.StartNew(()=> _unitOfWorks.SensorRepository.CreateSensorAsync(model, cancellationToken));
        Task.WaitAny(create);

        if (create.IsCompletedSuccessfully)
            return await Task.FromResult(true);
        else
            return await Task.FromResult(false);
    }

    public async Task<IList<PerformanceReportViewModel>?> GetReportsBySensorId(Guid sensorId, CancellationToken cancellationToken)
    {
        var reports = await _unitOfWorks.SensorRepository.GetSensorReportBySensorIdAsync(sensorId, cancellationToken);
        if (reports is not null)
            return await Task.FromResult(reports.Adapt<IList<PerformanceReportViewModel>>());
        else
            return await Task.FromResult<IList<PerformanceReportViewModel>?>(null);
    }

    public async Task<SensorViewModel?> GetSensorInfoByIdAsync(Guid sensorId, CancellationToken cancellationToken)
    {
        var sensor = await _unitOfWorks.SensorRepository.GetSensorByIdAsync(sensorId, cancellationToken);
        if (sensor is not null)
            return await Task.FromResult(sensor.Adapt<SensorViewModel>());
        else
            return await Task.FromResult<SensorViewModel?>(null);
    }

    public async Task<bool?> RemoveRangeReportsBySensorIdAsync(Guid sensorId, CancellationToken cancellationToken)
    {
        Task removeRes = Task.Factory.StartNew(()=> _unitOfWorks.SensorRepository.ClearReportsBySensorIdAsync(sensorId, cancellationToken));
        Task.WaitAny(removeRes);

        if (removeRes.IsCompletedSuccessfully)
            return await Task.FromResult(true);
        else
            return await Task.FromResult(false);
    }

    public async Task<bool?> RemoveSensorAsync(Guid sensorId, CancellationToken cancellationToken)
    {
        Task removeSensor = Task.Factory.StartNew(()=> _unitOfWorks.SensorRepository.DeleteSensorByIdAsync(sensorId, cancellationToken));
        Task.WaitAny(removeSensor);

        if (removeSensor.IsCompletedSuccessfully)
            return await Task.FromResult(true);
        else
            return await Task.FromResult(false);
    }

    public async Task<bool?> UpdateSensorInfoAsync(SensorViewModel sensor, CancellationToken cancellationToken)
    {
        Sensor model = sensor.Adapt<Sensor>();
        Task update = Task.Factory.StartNew(() => _unitOfWorks.SensorRepository.UpdateSensorAsync(model, cancellationToken),cancellationToken);
        Task.WaitAny(update);

        if (update.IsCompletedSuccessfully)
            return await Task.FromResult(true);
        else
            return await Task.FromResult(false);
    }
}
