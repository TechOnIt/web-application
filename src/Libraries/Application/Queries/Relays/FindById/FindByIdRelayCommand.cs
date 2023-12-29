using TechOnIt.Application.Common.Models.ViewModels.Relay;

namespace TechOnIt.Application.Queries.Relays.FindById;

public class FindByIdRelayCommand : IRequest<object>
{
    public Guid RelayId { get; set; }
}

public class FindByIdRelayCommandHandler : IRequestHandler<FindByIdRelayCommand, object>
{
    #region constructure
    private readonly IUnitOfWorks _unitOfWorks;
    public FindByIdRelayCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion
    public async Task<object> Handle(FindByIdRelayCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var getRelay = await _unitOfWorks.RelayRepositry.FindByIdAsNoTrackingAsync(request.RelayId, cancellationToken);

            if (getRelay is null)
                return ResultExtention.NotFound($"cant find relay with id : {request.RelayId}");

            return getRelay.Adapt<RelayViewModel>();
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}