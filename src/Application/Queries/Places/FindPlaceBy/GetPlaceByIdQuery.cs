using TechOnIt.Application.Common.Models.ViewModels.Places;

namespace TechOnIt.Application.Queries.Places.FindPlaceBy;

public class GetPlaceByIdQuery : IRequest<object>
{
    public Guid Id { get; set; }
}

public class GetPlaceByIdQueryHandler : IRequestHandler<GetPlaceByIdQuery, object>
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public GetPlaceByIdQueryHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion
    public async Task<object> Handle(GetPlaceByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var getPlace = await _unitOfWorks.StructureRepository.GetPlaceByIdAsyncAsNoTracking(request.Id,cancellationToken);
            if (getPlace is null)
                return ResultExtention.NotFound($"can not find place with id : {request.Id}");

            var model = getPlace.Adapt<PlaceViewModel>();
            return model;
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}