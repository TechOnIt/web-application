using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Domain.Entities.Catalogs;

namespace TechOnIt.Application.Commands.Structures.Management.CreateStructure;

public class CreateStructureCommand : IRequest<object>, ICommittableRequest
{
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public int Type { get; set; }
}

public class CreateStructureCommandHandler : IRequestHandler<CreateStructureCommand, object>
{
    #region Ctor
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;
    private readonly ILogger<CreateStructureCommandHandler> _logger;

    public CreateStructureCommandHandler(IUnitOfWorks unitOfWorks,
        IMediator mediator,
        ILogger<CreateStructureCommandHandler> logger)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
        _logger = logger;
    }
    #endregion

    public async Task<object> Handle(CreateStructureCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // hash the password
            var structurePasswordHash = PasswordHash.Parse(request.Password);
            // Convert int to structure enumeration.
            StructureType structureType = Enumeration.FromValue<StructureType>(request.Type);
            // Create new structure object.
            var newStructure = new StructureEntity(request.Name, structurePasswordHash, request.UserId, structureType);
            await _unitOfWorks.StructureRepository.CreateAsync(newStructure, cancellationToken);

            await _mediator.Publish(new StructureNotifications(), cancellationToken);
            return ResultExtention.ConcurrencyResult(newStructure.ApiKey);
        }
        catch (Exception exp)
        {
            _logger.LogError(exp.Message);
            throw new Exception(exp.Message);
        }
    }
}

public class CreateStructureValidation : BaseFluentValidator<CreateStructureCommand>
{
    public CreateStructureValidation()
    {
        RuleFor(a => a.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(100)
            ;

        RuleFor(a => a.Type)
            .NotEmpty()
            .NotNull()
            ;
    }
}