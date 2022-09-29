using iot.Domain.Entities.Product.SensorAggregate;
using iot.Infrastructure.Persistence.Context.Identity;
using Microsoft.EntityFrameworkCore;

namespace iot.Infrastructure.Repositories.SQL.SensorAggregate;

public class SensorRepository : ISensorRepository
{
    #region constructor
    private readonly IdentityContext _context;
    public SensorRepository(IdentityContext identityContext)
    {
        _context = identityContext;
    }

    #endregion

    public async Task CreateSensorAsync(Sensor sensor, CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            await _context.Sensors.AddAsync(sensor,cancellationToken);
            await Task.CompletedTask;
        }

        cancellationToken.ThrowIfCancellationRequested();
    }
    public async Task DeleteSensorByIdAsync(Guid sensorId, CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            var sensor=await _context.Sensors.FirstAsync(s => s.Id == sensorId);
            if (sensor is null)
                throw new NullReferenceException($"can not find sensor by id : {sensorId} in database");

            _context.Sensors.Remove(sensor);
            await Task.CompletedTask;
        }

        cancellationToken.ThrowIfCancellationRequested();
    }
    public async Task<Sensor?> GetSensorByIdAsync(Guid sensorId, CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            var sensor = await _context.Sensors
                .AsNoTracking()
                .FirstOrDefaultAsync(a=>a.Id== sensorId, cancellationToken);

            if (sensor is null)
                throw new NullReferenceException($"can not find sensor by id : {sensorId} in database");

            return sensor;
        }

        cancellationToken.ThrowIfCancellationRequested();
        return null;
    }
    public async Task UpdateSensorAsync(Sensor sensor, CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            var findSensor = await _context.Sensors.FindAsync(sensor.Id,cancellationToken);
            if (findSensor is null)
                throw new NullReferenceException($"can not find sensor with id : {sensor.Id} in system");

            findSensor.SetSensorType(sensor.SensorType);
            findSensor.PlaceId= sensor.PlaceId;

            _context.Sensors.Update(findSensor);
            await Task.CompletedTask;
        }

        cancellationToken.ThrowIfCancellationRequested();
    }

    public async Task ClearReportsBySensorIdAsync(Guid sensorId, CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            var sensor = await _context.Sensors.FirstOrDefaultAsync(a => a.Id == sensorId,cancellationToken);
            if (sensor is null)
                throw new NullReferenceException($"can not find sensor by id : {sensorId} in database");

            sensor.ClearReports();
            _context.Entry(sensor).State = EntityState.Modified;

            await Task.CompletedTask;
        }

        cancellationToken.ThrowIfCancellationRequested();
    }
    public async Task DeleteReportByIdAsync(Guid sensorId, PerformanceReport report, CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            var sensor = await _context.Sensors.FirstOrDefaultAsync(a=>a.Id==sensorId, cancellationToken);
            if (sensor is null)
                throw new NullReferenceException($"can not find sensor by id : {sensorId} in database");

            sensor.RemoveReport(report);
            _context.Entry(sensor).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        cancellationToken.ThrowIfCancellationRequested();
    }
    public async Task<PerformanceReport?> FindRepoprtByIdAsync(Guid reportId, CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            var report = await _context.PerformanceReports.FindAsync(reportId, cancellationToken);
            if (report is null)
                throw new NullReferenceException($"can not find Report by id : {reportId} in database");

            return report;
        }

        cancellationToken.ThrowIfCancellationRequested();
        return null;
    }
    public async Task<IList<PerformanceReport>?> GetSensorReportBySensorIdAsync(Guid sensorId, CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            var sensor = await _context.Sensors.FindAsync(sensorId,cancellationToken);
            if (sensor is null)
                throw new NullReferenceException($"can not find sensor by id : {sensorId} in database");

            return sensor.GetReports() as List<PerformanceReport>;
        }

        cancellationToken.ThrowIfCancellationRequested();
        return null;
    }
}
