using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Application.Common.Interfaces;

namespace TechOnIt.Application.Commands.Place.UpdatePlace;

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
    private readonly IStructureAggeregateService _structureAggeregateService;
    private readonly IMediator _mediator;
    public UpdatePlaceCommandHandler(IStructureAggeregateService structureAggeregateService, IMediator mediator)
    {
        _structureAggeregateService = structureAggeregateService;
        _mediator = mediator;
    }
    #endregion

    public async Task<object> Handle(UpdatePlaceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var viewModel = new Common.Models.ViewModels.Places.PlaceUpdateViewModel(request.Id,request.Name,request.Description,request.StuctureId);
            var updateResult = await _structureAggeregateService.UpdatePlaceAsync(request.StuctureId,viewModel,cancellationToken);

            if(updateResult is null)
                return ResultExtention.Failed($"can not find place with id : {request.Id}");

            await _mediator.Publish(new PlaceNotifications());

            return ResultExtention.IdResult(request.Id);
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}