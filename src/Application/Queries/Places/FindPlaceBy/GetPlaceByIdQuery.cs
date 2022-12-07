using TechOnIt.Domain.Entities.Product.StructureAggregate;
using TechOnIt.Infrastructure.Repositories.UnitOfWorks;
using Mapster;
using TechOnIt.Application.Common.Models.ViewModels.Places;

namespace TechOnIt.Application.Queries.Places.FindPlaceBy;

public class GetPlaceByIdQuery : IRequest<Result<PlaceViewModel>>
{
    public Guid Id { get; set; }
}

public class GetPlaceByIdQueryHandler : IRequestHandler<GetPlaceByIdQuery, Result<PlaceViewModel>>
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public GetPlaceByIdQueryHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion
    public async Task<Result<PlaceViewModel>> Handle(GetPlaceByIdQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var getPlace = await _unitOfWorks.SqlRepository<Place>()
                .TableNoTracking.FirstOrDefaultAsync(a => a.Id == request.Id);

            if (getPlace is null)
                return Result.Fail($"can not find place with id : {request.Id}");

            var model = getPlace.Adapt<PlaceViewModel>();
            return Result.Ok(model);
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}