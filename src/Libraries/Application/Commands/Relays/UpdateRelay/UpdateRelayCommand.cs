using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Domain.Enums;

namespace TechOnIt.Application.Commands.Relays.UpdateRelay;

public class UpdateRelayCommand : IRequest<object>, ICommittableRequest
{
    public Guid RelayId { get; set; }
    public int Pin { get; set; }
    public RelayType RelayType { get; set; }
    public bool IsHigh { get; set; }
    public Guid PlaceId { get; set; }
}

public class UpdateRelayCommandHandler : IRequestHandler<UpdateRelayCommand, object>
{
    #region Constructure
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;
    public UpdateRelayCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }

    #endregion

    public async Task<object> Handle(UpdateRelayCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var updateResult =
                Task.Factory
                .StartNew(() => _unitOfWorks.RelayRepositry.UpdateAsync(request.Adapt<Domain.Entities.RelayEntity>(), cancellationToken)
                , cancellationToken);

            Task.WaitAny(updateResult);

            if (updateResult.IsFaulted)
                return await Task.FromResult(ResultExtention.Failed("relay can not be found !"));

            await _mediator.Publish(new RelayNotifications());

            return await Task.FromResult(ResultExtention.IdResult(request.RelayId));
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}