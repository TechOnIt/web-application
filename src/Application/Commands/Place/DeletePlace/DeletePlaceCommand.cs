using iot.Application.Common.Interfaces;
using iot.Application.Events.ProductNotifications;

namespace iot.Application.Commands.Place.DeletePlace;

public class DeletePlaceCommand : IRequest<Result<Guid>>,ICommittableRequest
{
    public Guid Id { get; set; }
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
	public async Task<Result<Guid>> Handle(DeletePlaceCommand request, CancellationToken cancellationToken)
    {
		var repo = _unitOfWorks.SqlRepository<Domain.Entities.Product.StructureAggregate.Place>();

		try
		{
			var place = await repo.Table.FirstOrDefaultAsync(a => a.Id == request.Id);
			if (place is null)
				return Result.Fail($"can not find place with id : {request.Id}");

			await repo.DeleteAsync(place);
			await _mediator.Publish(new PlaceNotifications());
			return Result.Ok(request.Id);
		}
		catch (Exception exp)
		{
			return Result.Fail($" error : {exp.Message}");
		}
    }
}