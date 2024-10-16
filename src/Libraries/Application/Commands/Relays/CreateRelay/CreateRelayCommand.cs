using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Domain.Entities.Controllers;

namespace TechOnIt.Application.Commands.Relays.CreateRelay;

public class CreateRelayCommand : IRequest<object>, ICommittableRequest
{
    public int Pin { get; set; }
    public RelayType RelayType { get; set; } = RelayType.Light;
    public Guid GroupId { get; set; }
}

public class CreateRelayCommandHandler : IRequestHandler<CreateRelayCommand, object>
{
    #region Ctor
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;

    public CreateRelayCommandHandler(IMediator mediator, IUnitOfWorks unitOfWorks)
    {
        _mediator = mediator;
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<object> Handle(CreateRelayCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var model = new RelayEntity(request.Pin, request.RelayType, request.GroupId);
            Task createResult = Task.Factory.StartNew(() => _unitOfWorks.RelayRepositry.CreateAsync(model, cancellationToken), cancellationToken);
            Task.WaitAny(createResult);

            await _mediator.Publish(new RelayNotifications());

            if (!createResult.IsCompletedSuccessfully)
                return ResultExtention.Failed("an error occured !");
            else
                return ResultExtention.IdResult(model.Id);
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}