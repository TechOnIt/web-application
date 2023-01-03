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
            var structure = new StructureViewModel(Guid.NewGuid(), request.Name, request.Description,true, request.Type, DateTime.Now);

            var createRes = await _structureAggeregateService.CreateStructureAsync(structure,cancellationToken);

            if (createRes is null)
                return ResultExtention.Failed($"an error occared !");

            await _mediator.Publish(new StructureNotifications(), cancellationToken);
            return ResultExtention.ConcurrencyResult(model.ApiKey);
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}