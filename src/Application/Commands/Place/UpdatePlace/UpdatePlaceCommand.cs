using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Infrastructure.Repositories.UnitOfWorks;
using TechOnIt.Application.Common.Interfaces;

namespace TechOnIt.Application.Commands.Place.UpdatePlace;

public class UpdatePlaceCommand : IRequest<Result<Guid>>, ICommittableRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Guid StuctureId { get; set; }
}

public class UpdatePlaceCommandHandler : IRequestHandler<UpdatePlaceCommand, Result<Guid>>
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

    public async Task<Result<Guid>> Handle(UpdatePlaceCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var getPlace = await _unitOfWorks.StructureRepository.GetPlaceByIdAsync(request.Id, cancellationToken);
            if (getPlace is null)
                return Result.Fail($"can not find place with id : {request.Id}");

            getPlace.StuctureId = request.StuctureId;
            getPlace.Name = request.Name;
            getPlace.Description = request.Description;
            getPlace.SetModifyDate();

            await _unitOfWorks.StructureRepository.UpdatePlaceAsync(request.StuctureId, getPlace, cancellationToken);

            await _mediator.Publish(new PlaceNotifications());
            return Result.Ok(request.Id);
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}