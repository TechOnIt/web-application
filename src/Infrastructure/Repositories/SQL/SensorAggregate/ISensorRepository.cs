using TechOnIt.Domain.Entities.Product.SensorAggregate;

namespace TechOnIt.Infrastructure.Repositories.SQL.SensorAggregate;

public interface ISensorRepository
{
    #region sensor
    Task CreateSensorAsync(Sensor sensor, CancellationToken cancellationToken = default);
    Task<(bool Result, bool IsExists)> UpdateSensorAsync(Sensor sensor, CancellationToken cancellationToken);
    Task<(bool Result, bool IsExists)> DeleteSensorByIdAsync(Guid sensorId, CancellationToken cancellationToken);
    Task<Sensor?> GetSensorByIdAsync(Guid sensorId, CancellationToken cancellationToken = default);

    #endregion

    #region report
    Task<IList<PerformanceReport>?> GetSensorReportBySensorIdAsync(Guid sensorId, CancellationToken cancellationToken = default);
    Task ClearReportsBySensorIdAsync(Guid sensorId, CancellationToken cancellationToken = default);
    Task DeleteReportByIdAsync(Guid sensorId, Guid reportId, CancellationToken cancellationToken);
    Task<PerformanceReport?> FindRepoprtByIdAsync(Guid reportId, CancellationToken cancellationToken = default);
    Task AddReportToSensorAsync(PerformanceReport model, CancellationToken cancellationToken);
    #endregion
}