using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Domain.Entities.Catalog;

namespace TechOnIt.Application.Commands.Groups.UpdateGroup;

public class UpdateGroupCommand : IRequest<object>, ICommittableRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; } = null;
    public string? Description { get; set; } = null;
    public Guid StuctureId { get; set; }
}

public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, object>
{
    #region Ctor

    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;

    public UpdateGroupCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }

    #endregion

    public async Task<object> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Task updateGroup =
                Task.Factory
                .StartNew(() =>
                _unitOfWorks.StructureRepository.UpdateGroupAsync(request.StuctureId, request.Adapt<Group>())
                , cancellationToken);

            await updateGroup;

            if (updateGroup.IsFaulted)
                return await Task.FromResult(ResultExtention.Failed($"can not find group with id : {request.Id}"));

            await _mediator.Publish(new GroupNotifications());

            return await Task.FromResult(ResultExtention.IdResult(request.Id));
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}