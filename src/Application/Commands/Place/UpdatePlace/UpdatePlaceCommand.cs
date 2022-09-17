using iot.Application.Common.Interfaces;
using iot.Application.Events.ProductNotifications;

namespace iot.Application.Commands.Place.UpdatePlace;

public class UpdatePlaceCommand : IRequest<Result<Guid>>,ICommittableRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
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

    public async Task<Result<Guid>> Handle(UpdatePlaceCommand request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWorks.SqlRepository<Domain.Entities.Product.StructureAggregate.Place>();

        try
        {
            var getPlace = await repo.Table.FirstOrDefaultAsync(a => a.Id == request.Id);
            if (getPlace is null)
                return Result.Fail($"can not find place with id : {request.Id}");

            getPlace.StuctureId = request.StuctureId;
            getPlace.Name=request.Name;
            getPlace.Description = request.Description;
            getPlace.SetModifyDate();

            await repo.UpdateAsync(getPlace);
            await _mediator.Publish(new PlaceNotifications());
            return Result.Ok(request.Id);
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}