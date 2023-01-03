using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Application.Common.Interfaces;

namespace TechOnIt.Application.Commands.Place.DeletePlace;

public class DeletePlaceCommand : IRequest<object>, ICommittableRequest
{
    public Guid Id { get; set; }
    public Guid StructureId { get; set; }
}

public class DeletePlaceCommandHandler : IRequestHandler<DeletePlaceCommand, object>
{
    #region constructore
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;

    public DeletePlaceCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }

    #endregion

    public async Task<object> Handle(DeletePlaceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            bool DeletePlace = await _unitOfWorks.StructureRepository.DeletePlaceAsync(request.Id, request.StructureId, cancellationToken);
            if(!DeletePlace)
                return ResultExtention.Failed($"can not find place with id : {request.Id}");

            await _mediator.Publish(new PlaceNotifications());
            return ResultExtention.BooleanResult(DeletePlace);
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}