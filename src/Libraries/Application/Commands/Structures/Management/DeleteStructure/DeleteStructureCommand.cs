using TechOnIt.Application.Common.Interfaces;

namespace TechOnIt.Application.Commands.Structures.Management.DeleteStructure;

public class DeleteStructureCommand : IRequest<object>, ICommittableRequest
{
    public Guid StructureId { get; set; }
}

public class DeleteStructureCommandHandler : IRequestHandler<DeleteStructureCommand, object>
{
    #region Ctor
    private readonly IUnitOfWorks _unitOfWorks;
    public DeleteStructureCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<object> Handle(DeleteStructureCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var deleteRes = await _unitOfWorks.StructureRepository.DeleteByIdAsync(request.StructureId,cancellationToken);
            if (!deleteRes)
                return ResultExtention.Failed("Structure not found !");
            else
                return ResultExtention.BooleanResult(true);
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}