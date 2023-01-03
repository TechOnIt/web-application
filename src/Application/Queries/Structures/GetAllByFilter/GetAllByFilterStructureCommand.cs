using TechOnIt.Application.Common.Models.ViewModels.Structures;
using TechOnIt.Application.Reports.StructuresAggregate;
using Microsoft.AspNetCore.Mvc;

namespace TechOnIt.Application.Queries.Structures.GetAllByFilter;

public class GetAllByFilterStructureCommand : IRequest<Result<IList<StructureViewModel>>>
{

}

public class GetAllByFilterStructureCommandHandler : IRequestHandler<GetAllByFilterStructureCommand, object>
{
    #region Ctor
    private readonly IUnitOfWorks _unitOfWorks;
    public GetAllByFilterStructureCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion

    public async Task<object> Handle(GetAllByFilterStructureCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var getAll = await _unitOfWorks.StructureRepository.GetAllByFilterAsync(cancellationToken, null);
            return ResultExtention.ListResult(getAll);
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}