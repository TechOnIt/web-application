using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Application.Common.Interfaces;
using TechOnIt.Application.Common.Models.ViewModels.Places;

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
    private readonly IStructureAggeregateService _structureAggeregateService;
    private readonly IMediator _mediator;

    public CreatePlaceCommandHandler(IStructureAggeregateService structureAggeregateService, IMediator mediator)
    {
        _structureAggeregateService = structureAggeregateService;
        _mediator = mediator;
    }
    #endregion

    public async Task<object> Handle(CreatePlaceCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var newPlace = new PlaceCreateViewModel(request.Id, request.Name, request.Description, request.StuctureId);
            var createRes = await _structureAggeregateService.CreatePlaceAsync(request.StuctureId,newPlace,cancellationToken);
            if (createRes is null)
                return ResultExtention.Failed($"can not find structore with id : {request.StuctureId}");

            await _mediator.Publish(new PlaceNotifications());

            return ResultExtention.IdResult(createRes.Value);
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}