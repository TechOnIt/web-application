using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Domain.Enums;
using TechOnIt.Application.Common.Interfaces;
using TechOnIt.Domain.Entities.Product.StructureAggregate;

namespace TechOnIt.Application.Commands.Structures.Management.CreateStructure;

public class CreateStructureCommand : IRequest<object>, ICommittableRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public StuctureType Type { get; private set; }
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
            var model = new Structure(Guid.NewGuid(), request.Name, request.Description, DateTime.Now, DateTime.Now, request.Type);
            Task createStructure = Task.Factory
                .StartNew(()=>_unitOfWorks.StructureRepository.CreateAsync(model,cancellationToken)
                ,cancellationToken);

            await createStructure;

            if (createStructure.IsFaulted)
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