using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Application.Common.Interfaces;

namespace TechOnIt.Application.Commands.Place.CreatePlace;

public class CreatePlaceCommand : IRequest<object>, ICommittableRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Guid StuctureId { get; set; }
}

public class CreatePlaceCommandHandler : IRequestHandler<CreatePlaceCommand, object>
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;

    public CreatePlaceCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }
    #endregion

    public async Task<object> Handle(CreatePlaceCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            request.Id = request.Id == Guid.Empty ? Guid.NewGuid() : request.Id;

            Task createPlace =
                Task.Factory
                .StartNew(() => 
                _unitOfWorks.StructureRepository
                .CreatePlaceAsync(request.Adapt<TechOnIt.Domain.Entities.Product.StructureAggregate.Place>(),request.StuctureId,cancellationToken)
                , cancellationToken);

            await createPlace;

            if (createPlace.IsFaulted)
                return await Task.FromResult(ResultExtention.Failed($"can not find structore with id : {request.StuctureId}"));

            await _mediator.Publish(new PlaceNotifications());

            return await Task.FromResult(ResultExtention.IdResult(request.Id));
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}