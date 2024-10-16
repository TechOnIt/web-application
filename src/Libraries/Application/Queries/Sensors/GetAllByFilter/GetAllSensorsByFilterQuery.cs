using TechOnIt.Application.Common.Models.ViewModels.Sensors;

namespace TechOnIt.Application.Queries.Sensors.GetAllByFilter;

public class GetAllSensorsByFilterQuery : IRequest<object>
{
    public Guid GroupId { get; set; }
}

public class GetAllSensorsByFilterQueryHandler : IRequestHandler<GetAllSensorsByFilterQuery, object>
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public GetAllSensorsByFilterQueryHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion

    public async Task<object> Handle(GetAllSensorsByFilterQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var allSensors = await _unitOfWorks.SensorRepository.GetAllSensorsByGroupIdAsync(request.GroupId,cancellationToken);
            var result = allSensors.Adapt<IList<SensorViewModel>>();
            return ResultExtention.ListResult(result);
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}