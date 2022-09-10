using iot.Domain.Common;
using iot.Domain.Entities.Product;
using Mapster;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace iot.Application.Queries.Structures.GetAllByFilter;

public class GetAllByFilterStructureCommand : IRequest<Result<IList<StructureViewModel>>>
{
    public Expression<Func<Structure, bool>>? Filter { get; set; }
    public Func<IQueryable, IOrderedQueryable<Structure>>? Ordered { get; set; }
    public Expression<Func<Structure, object>>[]? IncludesTo { get; set; }
}

public class GetAllByFilterStructureCommandHandler : IRequestHandler<GetAllByFilterStructureCommand, Result<IList<StructureViewModel>>>
{
    #region constructure
    private readonly IUnitOfWorks _unitOfWorks;

    public GetAllByFilterStructureCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion

    public async Task<Result<IList<StructureViewModel>>> Handle(GetAllByFilterStructureCommand request, CancellationToken cancellationToken)
    {
        var allStructures = _unitOfWorks.SqlRepository<Structure>().TableNoTracking.AsQueryable();
        if (allStructures.Any())
        {
            if (request.IncludesTo != null)
            {
                foreach (var item in request.IncludesTo)
                {
                    allStructures = allStructures.Include(item);
                }
            }

            if (request.Filter != null)
            {
                allStructures = allStructures.Where(request.Filter);
            }

            if (request.Ordered != null)
            {
                allStructures = request.Ordered(allStructures);
            }
        }

        var list = await allStructures.ToListAsync();
        var result = list.Adapt<IList<StructureViewModel>>();
        return Result.Ok(result);
    }
}