using iot.Application.Common.ViewModels;
using iot.Domain.Entities.Product.StructureAggregate;
using iot.Infrastructure.Repositories.UnitOfWorks;
using Mapster;
using System.Linq.Expressions;

namespace iot.Application.Queries.Places.GetAllPlaceByFilter;

public class GetAllPlacesByFilterQuery : IRequest<Result<IList<PlaceViewModel>>>
{
    public Expression<Func<Place, bool>>? Filter { get; set; }
    public Func<IQueryable, IOrderedQueryable<Place>>? Ordered { get; set; }
    public Expression<Func<Place, object>>[]? IncludesTo { get; set; }
}

public class GetAllPlacesByFilterQueryHandler : IRequestHandler<GetAllPlacesByFilterQuery, Result<IList<PlaceViewModel>>>
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public GetAllPlacesByFilterQueryHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion

    public async Task<Result<IList<PlaceViewModel>>> Handle(GetAllPlacesByFilterQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var allPlaces = _unitOfWorks.SqlRepository<Place>().TableNoTracking.AsQueryable();
            if (allPlaces.Any())
            {
                if (request.IncludesTo != null)
                {
                    foreach (var item in request.IncludesTo)
                    {
                        allPlaces = allPlaces.Include(item);
                    }
                }

                if (request.Filter != null)
                {
                    allPlaces = allPlaces.Where(request.Filter);
                }

                if (request.Ordered != null)
                {
                    allPlaces = request.Ordered(allPlaces);
                }
            }

            var list = await allPlaces.ToListAsync(cancellationToken) as IList<Place>;
            var result = list.Adapt<IList<PlaceViewModel>>();
            return Result.Ok(result);
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}