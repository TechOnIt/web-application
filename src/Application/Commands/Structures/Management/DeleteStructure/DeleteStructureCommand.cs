using iot.Application.Common.Interfaces;
using iot.Infrastructure.Repositories.UnitOfWorks;

namespace iot.Application.Commands.Structures.Management.DeleteStructure;

public class DeleteStructureCommand : IRequest<Result> , ICommittableRequest
{
    public Guid StructureId { get; set; }
}

public class DeleteStructureCommandHandler : IRequestHandler<DeleteStructureCommand, Result>
{
    #region Structure
    private readonly IUnitOfWorks _unitOfWorks;
    public DeleteStructureCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion

    public async Task<Result> Handle(DeleteStructureCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var findStructure = await _unitOfWorks.StructureRepository.GetStructureByIdAsync(request.StructureId,cancellationToken);
            if (findStructure != null)
            {
                _unitOfWorks.StructureRepository.DeleteStructure(findStructure);
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