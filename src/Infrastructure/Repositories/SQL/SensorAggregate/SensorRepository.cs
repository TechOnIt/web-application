using TechOnIt.Domain.Entities.SensorAggregate;
using TechOnIt.Infrastructure.Persistence.Context;

namespace TechOnIt.Infrastructure.Repositories.SQL.SensorAggregate;

public class SensorRepository : ISensorRepository
{
    #region Ctor
    private readonly IdentityContext _context;
    public SensorRepository(IdentityContext context)
    {
        _context = context;
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

        cancellationToken.ThrowIfCancellationRequested();
        _context.Sensors.Update(findSensor);
        return (true, true);
    }
    public async Task<IList<SensorReport>?> GetSensorReportBySensorIdAsync(Guid sensorId, CancellationToken cancellationToken)
    => await Task.FromResult<IList<SensorReport>?>(await _context.SensorReports.Where(a => a.SensorId == sensorId).ToListAsync(cancellationToken));

    public async Task<IList<SensorReport>?> GetSensorReportBySensorIdAsNoTrackingAsync(Guid sensorId, CancellationToken cancellationToken)
    => await _context.SensorReports.AsNoTracking().Where(a => a.SensorId == sensorId).ToListAsync(cancellationToken);

    public async Task<IList<SensorReport>?> GetSensorReportBySensorIdAsNoTrackingWithTimeFilterAsync(Guid sensorId, DateTime minTime, DateTime maxTime, CancellationToken cancellationToken)
        => await _context
            .SensorReports
            .AsNoTracking()
            .Where(a => a.SensorId == sensorId && a.CreatedAt > minTime && a.CreatedAt <= maxTime)
            .ToListAsync(cancellationToken);


    public async Task<IList<Sensor>> GetAllSensorsByPlaceIdAsync(Guid placeId, CancellationToken cancellationToken)
        => await _context.Sensors.AsNoTracking().Where(a => a.PlaceId == placeId).ToListAsync();
    #endregion

    #region Report
    public async Task<SensorReport?> FindRepoprtByIdAsync(Guid reportId, CancellationToken cancellationToken)
        => await Task.FromResult(await _context.SensorReports.FirstOrDefaultAsync(a => a.Id == reportId, cancellationToken));
    public async Task AddReportToSensorAsync(SensorReport model, CancellationToken cancellationToken)
    {
        var sensor = await _context.Sensors.FirstOrDefaultAsync(a => a.Id == model.SensorId);

        if (sensor is not null)
            sensor.AddReport(model);

        await Task.CompletedTask;
    }

    #endregion
}
