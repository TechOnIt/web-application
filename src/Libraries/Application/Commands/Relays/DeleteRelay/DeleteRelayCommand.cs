using TechOnIt.Application.Events.ProductNotifications;

namespace TechOnIt.Application.Commands.Relays.DeleteRelay;

public class DeleteRelayCommand : IRequest<object>
{
    public Guid RelayId { get; set; }
}

public class DeleteRelayCommandHandler : IRequestHandler<DeleteRelayCommand, object>
{
    #region constructure
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;
    public DeleteRelayCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }

    #endregion

    public async Task<object> Handle(DeleteRelayCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Task getRelay = Task.Factory.StartNew(() => _unitOfWorks.RelayRepositry.DeleteByIdAsync(request.RelayId, cancellationToken), cancellationToken);
            Task.WaitAny(getRelay);

            if (!getRelay.IsCompletedSuccessfully)
                return await Task.FromResult(ResultExtention.Failed($"can not find relay with Id : {request.RelayId}"));

            await _mediator.Publish(new RelayNotifications());

            return await Task.FromResult(ResultExtention.BooleanResult(true));
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}