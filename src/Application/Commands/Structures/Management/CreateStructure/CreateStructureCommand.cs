using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Application.Common.Interfaces;
using TechOnIt.Domain.Entities.Product.StructureAggregate;

namespace TechOnIt.Application.Commands.Structures.Management.CreateStructure;

public class CreateStructureCommand : IRequest<object>, ICommittableRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public int Type { get; set; }
}

public class CreateStructureCommandHandler : IRequestHandler<CreateStructureCommand, object>
{
    #region Ctor
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;

    public CreateStructureCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }

    #endregion

    public async Task<object> Handle(CreateStructureCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var model = new Structure(Guid.NewGuid(),request.UserId, request.Name, request.Description, DateTime.Now, DateTime.Now, request.Type);
            await _unitOfWorks.StructureRepository.CreateAsync(model, cancellationToken);

            await _mediator.Publish(new StructureNotifications(), cancellationToken);
            return ResultExtention.ConcurrencyResult(model.ApiKey);
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}