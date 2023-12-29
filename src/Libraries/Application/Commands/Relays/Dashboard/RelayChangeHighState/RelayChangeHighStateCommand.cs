namespace TechOnIt.Application.Commands.Relays.Dashboard.RelayChangeHighState;

public class RelayChangeHighStateCommand : IRequest<bool>
{
    public Guid RelayId { get; set; }
    public bool IsHigh { get; set; }
}

public class RelayChangeHighStateCommandHandler : IRequestHandler<RelayChangeHighStateCommand, bool>
{
    #region Ctor
    private readonly IUnitOfWorks _unitOfWork;

    public RelayChangeHighStateCommandHandler(IUnitOfWorks unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    #endregion

    public async Task<bool> Handle(RelayChangeHighStateCommand request, CancellationToken cancellationToken)
    {
        var relay = await _unitOfWork.RelayRepositry.FindByIdAsync(request.RelayId, cancellationToken);
        if (relay is null)
            return false;
        if (request.IsHigh)
            relay.High();
        else
            relay.Low();
        await _unitOfWork.RelayRepositry.UpdateAsync(relay, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
        return true;
    }
}