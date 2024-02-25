using TechOnIt.Application.Common.Models.ViewModels.Groups;

namespace TechOnIt.Application.Queries.Groups.GetAllGroupByFilter;

public class GetAllGroupByFilterQuery : PaginatedSearchWithSize, IRequest<Result>
{
}

public class GetAllGroupsByFilterQueryHandler : IRequestHandler<GetAllGroupByFilterQuery, Result>
{
    #region DI

    private readonly IUnitOfWorks _unitOfWorks;
    public GetAllGroupsByFilterQueryHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion

    public async Task<Result> Handle(GetAllGroupByFilterQuery request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var allGroups = await _unitOfWorks.StructureRepository.GetAllPlcaesByFilterAsync(cancellationToken);
            var result = allGroups.Adapt<IList<GroupViewModel>>();
            return Result.Ok(result);
        }
        catch (Exception exp)
        {
            return Result.Fail(exp.Message);
        }
    }
}