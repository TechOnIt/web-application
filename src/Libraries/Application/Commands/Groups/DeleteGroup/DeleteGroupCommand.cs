using TechOnIt.Application.Events.ProductNotifications;

namespace TechOnIt.Application.Commands.Groups.DeleteGroup;

public class DeleteGroupCommand : IRequest<object>, ICommittableRequest
{
    public Guid Id { get; set; }
    public Guid StructureId { get; set; }
}

public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, object>
{
    #region constructore
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;

    public DeleteGroupCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }

    #endregion

    public async Task<object> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        try
        {
            bool DeleteGroup = await _unitOfWorks.StructureRepository.DeleteGroupAsync(request.Id, request.StructureId, cancellationToken);
            if (!DeleteGroup)
                return ResultExtention.Failed($"can not find group with id : {request.Id}");

            await _mediator.Publish(new GroupNotifications());
            return ResultExtention.BooleanResult(DeleteGroup);
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}