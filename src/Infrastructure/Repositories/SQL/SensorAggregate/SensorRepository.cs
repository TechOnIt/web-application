using TechOnIt.Domain.Entities.Product.SensorAggregate;
using Microsoft.EntityFrameworkCore;
using TechOnIt.Infrastructure.Persistence.Context;

namespace TechOnIt.Infrastructure.Repositories.SQL.SensorAggregate;

public class SensorRepository : ISensorRepository
{
    #region constructor
    private readonly IdentityContext _context;
    public SensorRepository(IdentityContext identityContext)
    {
        _context = identityContext;
    }
    #endregion

    #region sensor
    public async Task CreateSensorAsync(Sensor sensor, CancellationToken cancellationToken)
    {
        await _context.Sensors.AddAsync(sensor, cancellationToken);
        await Task.CompletedTask;
    }
    public async Task<(bool Result, bool IsExists)> DeleteSensorByIdAsync(Guid sensorId, CancellationToken cancellationToken)
    {
        var sensor = await _context.Sensors.FirstOrDefaultAsync(s => s.Id == sensorId, cancellationToken);

        if (sensor == null)
            return (false, false);

        _context.Sensors.Remove(sensor);
        return (true, true);
    }
    public async Task<Sensor?> GetSensorByIdAsync(Guid sensorId, CancellationToken cancellationToken)
    {
        var sensor = await _context.Sensors
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == sensorId, cancellationToken);

        return await Task.FromResult(sensor);
    }
    public async Task<(bool Result, bool IsExists)> UpdateSensorAsync(Sensor sensor, CancellationToken cancellationToken)
    {
        var findSensor = await _context.Sensors.FindAsync(sensor.Id, cancellationToken);
        if (findSensor is null)
            return (false, false);

        findSensor.SetSensorType(sensor.SensorType);
        findSensor.PlaceId = sensor.PlaceId;

        cancellationToken.ThrowIfCancellationRequested();
        _context.Sensors.Update(findSensor);
        return (true, true);
    }
    public async Task<IList<PerformanceReport>?> GetSensorReportBySensorIdAsync(Guid sensorId, CancellationToken cancellationToken)
    => await Task.FromResult<IList<PerformanceReport>?>(await _context.PerformanceReports.Where(a => a.SensorId == sensorId).ToListAsync(cancellationToken));

    public async Task<IList<PerformanceReport>?> GetSensorReportBySensorIdAsNoTrackingAsync(Guid sensorId, CancellationToken cancellationToken)
    => await _context.PerformanceReports.AsNoTracking().Where(a => a.SensorId == sensorId).ToListAsync(cancellationToken);

    public async Task<IList<PerformanceReport>?> GetSensorReportBySensorIdAsNoTrackingWithTimeFilterAsync(Guid sensorId, DateTime minTime, DateTime maxTime, CancellationToken cancellationToken)
        => await _context
            .PerformanceReports
            .AsNoTracking()
            .Where(a => a.SensorId == sensorId && a.RecordDateTime > minTime && a.RecordDateTime <= maxTime)
            .ToListAsync(cancellationToken);


    public async Task<IList<Sensor>> GetAllSensorsByPlaceIdAsync(Guid placeId, CancellationToken cancellationToken)
        => await _context.Sensors.AsNoTracking().Where(a => a.PlaceId == placeId).ToListAsync();
    #endregion

    #region report
    public async Task ClearReportsBySensorIdAsync(Guid sensorId, CancellationToken cancellationToken)
    {
        var sensor = await _context.Sensors.Include(r => r.Reports).FirstOrDefaultAsync(a => a.Id == sensorId, cancellationToken);
        if (sensor != null)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                sensor.ClearReports();
                _context.Update(sensor);
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteReportByIdAsync(Guid sensorId, Guid reportId, CancellationToken cancellationToken)
    {
        var getReport = await _context.PerformanceReports
            .FirstOrDefaultAsync(a => a.Id == reportId && a.SensorId == sensorId);

        if (getReport is not null)
            if (!cancellationToken.IsCancellationRequested)
                _context.PerformanceReports.Remove(getReport);

        await Task.CompletedTask;
    }
    public async Task<PerformanceReport?> FindRepoprtByIdAsync(Guid reportId, CancellationToken cancellationToken)
        => await Task.FromResult(await _context.PerformanceReports.FirstOrDefaultAsync(a => a.Id == reportId, cancellationToken));
    public async Task AddReportToSensorAsync(PerformanceReport model, CancellationToken cancellationToken)
    {
        var sensor = await _context.Sensors.FirstOrDefaultAsync(a => a.Id == model.SensorId);

        if (sensor is not null)
            sensor.AddReport(model);

        await Task.CompletedTask;
    }

    #endregion
}
