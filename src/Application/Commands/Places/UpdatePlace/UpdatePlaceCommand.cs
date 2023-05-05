using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Application.Common.Interfaces;

namespace TechOnIt.Application.Commands.Places.UpdatePlace;

public class UpdatePlaceCommand : IRequest<object>, ICommittableRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Guid StuctureId { get; set; }
}

public class UpdatePlaceCommandHandler : IRequestHandler<UpdatePlaceCommand, object>
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;
    public UpdatePlaceCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }
    #endregion

    public async Task<object> Handle(UpdatePlaceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Task updatePlace =
                Task.Factory
                .StartNew(() =>
                _unitOfWorks.StructureRepository.UpdatePlaceAsync(request.StuctureId, request.Adapt<Place>())
                , cancellationToken);

            await updatePlace;

            if (updatePlace.IsFaulted)
                return await Task.FromResult(ResultExtention.Failed($"can not find place with id : {request.Id}"));

            await _mediator.Publish(new PlaceNotifications());

            return await Task.FromResult(ResultExtention.IdResult(request.Id));
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}