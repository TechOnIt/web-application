using iot.Domain.Entities.Product.SensorAggregate;

namespace iot.Infrastructure.Repositories.SQL.SensorAggregate;

public interface ISensorRepository
{
    Task CreateSensorAsync(Sensor sensor, CancellationToken cancellationToken);
    Task UpdateSensorAsync(Sensor sensor, CancellationToken cancellationToken);
    Task DeleteSensorByIdAsync(Guid sensorId, CancellationToken cancellationToken);
    Task<Sensor?> GetSensorByIdAsync(Guid sensorId, CancellationToken cancellationToken);
    Task<IList<PerformanceReport>?> GetSensorReportBySensorIdAsync(Guid sensorId, CancellationToken cancellationToken);
    Task ClearReportsBySensorIdAsync(Guid sensorId, CancellationToken cancellationToken);
    Task DeleteReportByIdAsync(Guid sensorId,PerformanceReport report, CancellationToken cancellationToken);

    Task<PerformanceReport?> FindRepoprtByIdAsync(Guid reportId, CancellationToken cancellationToken);
}