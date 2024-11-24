using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Domain.Entities.Catalogs;

namespace TechOnIt.Application.Commands.Groups.CreateGroup;

public class CreateGroupCommand : IRequest<object>, ICommittableRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; } = null;
    public string? Description { get; set; } = null;
    public Guid StuctureId { get; set; }
}

public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, object>
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;

    public CreateGroupCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }
    #endregion

    public async Task<object> Handle(CreateGroupCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            request.Id = request.Id == Guid.Empty ? Guid.NewGuid() : request.Id;

            Task createGroup =
                Task.Factory
                .StartNew(() =>
                _unitOfWorks.StructureRepository
                .CreateGroupAsync(request.Adapt<Group>(), request.StuctureId, cancellationToken)
                , cancellationToken);

            await createGroup;

            if (createGroup.IsFaulted)
                return await Task.FromResult(ResultExtention.Failed($"can not find structore with id : {request.StuctureId}"));

            await _mediator.Publish(new GroupNotifications());

            return await Task.FromResult(ResultExtention.IdResult(request.Id));
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}