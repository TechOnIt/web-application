using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Infrastructure.Repositories.UnitOfWorks;
using TechOnIt.Application.Common.Interfaces;

namespace TechOnIt.Application.Commands.Place.DeletePlace;

public class DeletePlaceCommand : IRequest<Result<Guid>>, ICommittableRequest
{
    public Guid Id { get; set; }
    public Guid StructureId { get; set; }
}

public class DeletePlaceCommandHandler : IRequestHandler<DeletePlaceCommand, Result<Guid>>
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

    public async Task<Result<Guid>> Handle(DeletePlaceCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var place = await _unitOfWorks.StructureRepository.GetPlaceByIdAsync(request.Id, cancellationToken);
            if (place is null)
                return Result.Fail($"can not find place with id : {request.Id}");

            await _unitOfWorks.StructureRepository.DeletePlaceAsync(request.StructureId, place, cancellationToken);

            await _mediator.Publish(new PlaceNotifications());
            return Result.Ok(request.Id);
        }
        catch (Exception exp)
        {
            return Result.Fail($" error : {exp.Message}");
        }
    }
}