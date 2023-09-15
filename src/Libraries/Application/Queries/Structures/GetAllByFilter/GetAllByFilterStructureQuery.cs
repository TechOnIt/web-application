namespace TechOnIt.Application.Queries.Structures.GetAllByFilter;

public class GetAllByFilterStructureQuery : IRequest<object>
{

}

public class GetAllByFilterStructureCommandHandler : IRequestHandler<GetAllByFilterStructureQuery, object>
{
    private readonly IUnitOfWorks _unitOfWorks;

    public GetAllByFilterStructureCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    public async Task<object> Handle(GetAllByFilterStructureQuery request, CancellationToken cancellationToken = default)
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