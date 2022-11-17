using iot.Domain.Entities.Product.SensorAggregate;

namespace iot.Infrastructure.Repositories.SQL.SensorAggregate;

public interface ISensorRepository
{
    Task CreateSensorAsync(Sensor sensor, CancellationToken cancellationToken = default);
    Task UpdateSensorAsync(Sensor sensor, CancellationToken cancellationToken = default);
    Task DeleteSensorByIdAsync(Guid sensorId, CancellationToken cancellationToken = default);
    Task<Sensor?> GetSensorByIdAsync(Guid sensorId, CancellationToken cancellationToken = default);
    Task<IList<PerformanceReport>?> GetSensorReportBySensorIdAsync(Guid sensorId, CancellationToken cancellationToken = default);
    Task ClearReportsBySensorIdAsync(Guid sensorId, CancellationToken cancellationToken = default);
    Task DeleteReportByIdAsync(Guid sensorId,PerformanceReport report, CancellationToken cancellationToken = default);

    Task<PerformanceReport?> FindRepoprtByIdAsync(Guid reportId, CancellationToken cancellationToken = default);
}