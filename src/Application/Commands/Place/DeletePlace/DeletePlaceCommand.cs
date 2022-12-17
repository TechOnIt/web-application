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
    private readonly IStructureAggeregateService _structureAggeregateService;
    private readonly IMediator _mediator;

    public DeletePlaceCommandHandler(IStructureAggeregateService structureAggeregateService, IMediator mediator)
    {
        _structureAggeregateService = structureAggeregateService;
        _mediator = mediator;
    }

    #endregion

    public async Task<object> Handle(DeletePlaceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var res = await _structureAggeregateService.DeletePlaceByStructureIdAsync(request.StructureId, request.Id, cancellationToken);
            if (res is null)
                return ResultExtention.Failed($"can not find place with id : {request.Id}");

            await _mediator.Publish(new PlaceNotifications());

            return ResultExtention.BooleanResult(res);
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}