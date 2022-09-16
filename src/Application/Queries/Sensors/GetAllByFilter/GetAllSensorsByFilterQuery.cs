using iot.Domain.Entities.Product;
using Mapster;
using System.Linq.Expressions;

namespace iot.Application.Queries.Sensors.GetAllByFilter;

public class GetAllSensorsByFilterQuery : IRequest<Result<IList<SensorViewModel>>>
{
    public Expression<Func<Sensor, bool>>? Filter { get; set; }
    public Func<IQueryable, IOrderedQueryable<Sensor>>? Ordered { get; set; }
    public Expression<Func<Sensor, object>>[]? IncludesTo { get; set; }
}

public class GetAllSensorsByFilterQueryHandler : IRequestHandler<GetAllSensorsByFilterQuery, Result<IList<SensorViewModel>>>
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public GetAllSensorsByFilterQueryHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion

    public async Task<Result<IList<SensorViewModel>>> Handle(GetAllSensorsByFilterQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var allSensors = _unitOfWorks.SqlRepository<Sensor>().TableNoTracking.AsQueryable();
            if (allSensors.Any())
            {
                if (request.IncludesTo != null)
                {
                    foreach (var item in request.IncludesTo)
                    {
                        allSensors = allSensors.Include(item);
                    }
                }

                if (request.Filter != null)
                {
                    allSensors = allSensors.Where(request.Filter);
                }

                if (request.Ordered != null)
                {
                    allSensors = request.Ordered(allSensors);
                }
            }

            var list = await allSensors.ToListAsync(cancellationToken) as IList<Sensor>;
            var result = list.Adapt<IList<SensorViewModel>>();
            return Result.Ok(result);
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}