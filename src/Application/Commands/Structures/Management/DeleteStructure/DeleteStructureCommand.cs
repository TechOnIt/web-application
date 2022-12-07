using TechOnIt.Application.Common.Interfaces;

namespace TechOnIt.Application.Commands.Structures.Management.DeleteStructure;

public class DeleteStructureCommand : IRequest<Result>, ICommittableRequest
{
    public Guid StructureId { get; set; }
}

public class DeleteStructureCommandHandler : IRequestHandler<DeleteStructureCommand, Result>
{
    #region Ctor
    private readonly IUnitOfWorks _unitOfWorks;

    public DeleteStructureCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<Result> Handle(DeleteStructureCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var findStructure = await _unitOfWorks.StructureRepository.GetByIdAsync(request.StructureId, cancellationToken);
            if (findStructure != null)
            {
                await _unitOfWorks.StructureRepository.DeleteAsync(findStructure, cancellationToken);
                return Result.Ok();
            }
            else
            {
                return Result.Fail("Structure not found !");
            }
        }
        catch (Exception exp)
        {
            return Result.Fail($"error ! : {exp.Message}");
        }
    }
}