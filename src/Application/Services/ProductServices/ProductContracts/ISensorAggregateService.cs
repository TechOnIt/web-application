using TechOnIt.Application.Common.Models.ViewModels.Reports;
using TechOnIt.Application.Common.Models.ViewModels.Sensors;
using TechOnIt.Domain.Entities.Product.SensorAggregate;

namespace TechOnIt.Application.Services.ProductServices.ProductContracts;

public interface ISensorAggregateService
{
    Task<bool?> CreateNewSensorAsync(SensorViewModel sensor, CancellationToken cancellationToken);
    Task<bool?> UpdateSensorInfoAsync(SensorViewModel sensor, CancellationToken cancellationToken);
    Task<bool?> RemoveSensorAsync(Guid sensorId,CancellationToken cancellationToken);
    Task<SensorViewModel?> GetSensorInfoByIdAsync(Guid sensorId,CancellationToken cancellationToken);
    Task<IList<PerformanceReportViewModel>?> GetReportsBySensorId(Guid sensorId, CancellationToken cancellationToken);

    Task<bool?> RemoveRangeReportsBySensorIdAsync(Guid sensorId, CancellationToken cancellationToken);
}