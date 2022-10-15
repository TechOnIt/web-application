using iot.Application.Common.ViewModels;
using iot.Domain.Entities.Product.SensorAggregate;
using iot.Infrastructure.Repositories.UnitOfWorks;

namespace iot.Application.Queries.Sensors.FindBy;

public class FindSesnsorByIdQuery : IRequest<Result<SensorViewModel>>
{
    public Guid Id { get; set; }
}

public class FindSesnsorByIdQueryHandler : IRequestHandler<FindSesnsorByIdQuery, Result<SensorViewModel>>
{
    #region constructure
    private readonly IUnitOfWorks _unitOfWorks;
    public FindSesnsorByIdQueryHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion
    public async Task<Result<SensorViewModel>> Handle(FindSesnsorByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var getSensor = await _unitOfWorks.SqlRepository<Sensor>()
                .TableNoTracking.FirstOrDefaultAsync(a => a.Id == request.Id);

            if (getSensor is null)
                return Result.Fail($"can not find sesnsor with id : {request.Id}");

            return Result.Ok(new SensorViewModel(getSensor.Id,getSensor.SensorType,getSensor.PlaceId));
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}