using TechOnIt.Application.Events.ProductNotifications;

namespace TechOnIt.Application.Commands.Sensors.DeleteSensor;

public class DeleteSensorCommand : IRequest<object>, ICommittableRequest
{
    public Guid Id { get; set; }
}

public class DeleteSensorCommandHandler : IRequestHandler<DeleteSensorCommand, object>
{
    #region constructure
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;

    public DeleteSensorCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }
    #endregion

    public async Task<object> Handle(DeleteSensorCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var deleteSensor = await _unitOfWorks.SensorRepository.DeleteSensorByIdAsync(request.Id, cancellationToken);

            if (!deleteSensor.IsExists)
                return await Task.FromResult(ResultExtention.Failed($"can not find sessor with id : {request.Id}"));

            if (!deleteSensor.Result)
                return await Task.FromResult(ResultExtention.Failed("error ocurred !"));

            await _mediator.Publish(new SensorNotifications());
            return await Task.FromResult(ResultExtention.Failed(request.Id));
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}