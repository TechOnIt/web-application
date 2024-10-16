using TechOnIt.Application.Events.ProductNotifications;

namespace TechOnIt.Application.Commands.Groups.DeleteGroup;

public class DeleteGroupCommand : IRequest<Result>, ICommittableRequest
{
    public Guid Id { get; set; }
    public Guid StructureId { get; set; }
}

public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, Result>
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

    public async Task<Result> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        try
        {
            bool isDeleteSuccess = await _unitOfWorks.StructureRepository.DeleteGroupAsync(request.Id, request.StructureId, cancellationToken);
            if (!isDeleteSuccess)
              //return ResultExtention.Failed($"can not find group with id : {request.Id}");
                return Result.Fail($"can not find group with id : {request.Id}");

            if (!isDeleteSuccess)
                return Result.Fail();

            await _mediator.Publish(new GroupNotifications());

            return Result.Ok();
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}