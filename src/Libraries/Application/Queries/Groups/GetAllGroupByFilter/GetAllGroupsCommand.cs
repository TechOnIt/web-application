using TechOnIt.Application.Common.Models.ViewModels.Groups;

namespace TechOnIt.Application.Queries.Groups.GetAllGroupByFilter;

public class GetAllGroupsCommand : IRequest<object>
{
}

public class GetAllGroupsByFilterQueryHandler : IRequestHandler<GetAllGroupsCommand, object>
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public GetAllGroupsByFilterQueryHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<object> Handle(GetAllGroupsCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var allGroups = _unitOfWorks.StructureRepository.GetAllGroupsByFilterAsync(cancellationToken);
            var result = allGroups.Adapt<IList<GroupViewModel>>();
            return ResultExtention.ListResult(result);
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}