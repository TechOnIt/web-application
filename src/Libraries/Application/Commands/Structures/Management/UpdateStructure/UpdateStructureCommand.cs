using TechOnIt.Domain.Enums;
using TechOnIt.Domain.Entities.Catalog;

namespace TechOnIt.Application.Commands.Structures.Management.UpdateStructure;

public class UpdateStructureCommand : IRequest<object>, ICommittableRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public StructureType Type { get; set; }
}

public class UpdatetructureCommandHandler : IRequestHandler<UpdateStructureCommand, object>
{
    #region Constructure
    private readonly IUnitOfWorks _unitOfWorks;
    public UpdatetructureCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<object> Handle(UpdateStructureCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = request.Adapt<Structure>();
            var updateResult = await _unitOfWorks.StructureRepository.UpdateAsync(model,cancellationToken);

            if (!updateResult)
                return ResultExtention.Failed("Structure not found !");

            return ResultExtention.BooleanResult(true);
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}