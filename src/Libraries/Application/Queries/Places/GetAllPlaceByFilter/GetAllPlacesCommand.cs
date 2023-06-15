using TechOnIt.Application.Common.Models.ViewModels.Places;

namespace TechOnIt.Application.Queries.Places.GetAllPlaceByFilter;

public class GetAllPlacesCommand : IRequest<object>
{
}

public class GetAllPlacesByFilterQueryHandler : IRequestHandler<GetAllPlacesCommand, object>
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public GetAllPlacesByFilterQueryHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<object> Handle(GetAllPlacesCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var allPlaces = _unitOfWorks.StructureRepository.GetAllPlcaesByFilterAsync(cancellationToken);
            var result = allPlaces.Adapt<IList<PlaceViewModel>>();
            return ResultExtention.ListResult(result);
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}