using TechOnIt.Application.Common.Models.ViewModels.Sensors;

namespace TechOnIt.Application.Queries.Sensors.FindBy;

public class FindSesnsorByIdQuery : IRequest<object>
{
    public Guid SensorId { get; set; }
}

public class FindSesnsorByIdQueryHandler : IRequestHandler<FindSesnsorByIdQuery, object>
{
    #region constructure
    private readonly IUnitOfWorks _unitOfWorks;
    public FindSesnsorByIdQueryHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion

    public async Task<object> Handle(FindSesnsorByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var getSensor = await _unitOfWorks.SensorRepository.GetSensorByIdAsync(request.SensorId,cancellationToken);

            if (getSensor is null)
                return ResultExtention.NotFound($"can not find sesnsor with id : {request.SensorId}");

            return new SensorViewModel(getSensor.Id, getSensor.Type, getSensor.PlaceId);
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}