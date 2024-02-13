using TechOnIt.Domain.Entities.SensorAggregate;

namespace TechOnIt.Infrastructure.Repositories.SQL.SensorAggregate;

public interface ISensorRepository
{
    #region Sensor
    Task CreateSensorAsync(SensorEntity sensor, CancellationToken cancellationToken = default);
    Task<(bool Result, bool IsExists)> UpdateSensorAsync(SensorEntity sensor, CancellationToken cancellationToken);
    Task<(bool Result, bool IsExists)> DeleteSensorByIdAsync(Guid sensorId, CancellationToken cancellationToken);
    Task<SensorEntity?> GetSensorByIdAsync(Guid sensorId, CancellationToken cancellationToken = default);
    Task<IList<SensorEntity>> GetAllSensorsByGroupIdAsync(Guid groupId, CancellationToken cancellationToken);
    Task<IList<SensorReportEntity>?> GetSensorReportBySensorIdAsNoTrackingAsync(Guid sensorId, CancellationToken cancellationToken);
    Task<IList<SensorReportEntity>?> GetSensorReportBySensorIdAsNoTrackingWithTimeFilterAsync(Guid sensorId, DateTime minTime, DateTime maxTime, CancellationToken cancellationToken);
    #endregion

    #region Report
    Task<IList<SensorReportEntity>?> GetSensorReportBySensorIdAsync(Guid sensorId, CancellationToken cancellationToken = default);
    Task<SensorReportEntity?> FindRepoprtByIdAsync(Guid reportId, CancellationToken cancellationToken = default);
    Task AddReportToSensorAsync(SensorReportEntity model, CancellationToken cancellationToken);
    #endregion
}