using TechOnIt.Application.Common.Models.ViewModels.Groups;

namespace TechOnIt.Application.Queries.Groups.FindGroupBy;

public class GetGroupByIdQuery : IRequest<object>
{
    public Guid Id { get; set; }
}

public class GetGroupByIdQueryHandler : IRequestHandler<GetGroupByIdQuery, object>
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public GetGroupByIdQueryHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion
    public async Task<object> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var getGroup = await _unitOfWorks.StructureRepository.GetGroupByIdAsyncAsNoTracking(request.Id,cancellationToken);
            if (getGroup is null)
                return ResultExtention.NotFound($"can not find group with id : {request.Id}");

            var model = getGroup.Adapt<GroupViewModel>();
            return model;
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}