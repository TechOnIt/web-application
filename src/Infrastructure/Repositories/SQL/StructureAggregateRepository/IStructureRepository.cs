using iot.Domain.Entities.Product.StructureAggregate;
using System.Linq.Expressions;

namespace iot.Application.Repositories.SQL.StructureAggregateRepository;

public interface IStructureRepository
{
    Task CreateStructureWithPlacesAsync(Structure structure,IList<Place> places,CancellationToken cancellationToken);
    Task CreateStructureAsync(Structure structure, CancellationToken cancellationToken);
    Task CreatePlaceAsync(Place place,Guid StructureId, CancellationToken cancellationToken);

    Task UpdatePlaceAsync(Guid structureId,Place place,CancellationToken cancellationToken);
    Task UpdateStructureAsync(Structure structure, CancellationToken cancellationToken);

    void DeleteStructure(Structure structure);
    Task DeleteStructureAsync(Guid structureId, CancellationToken cancellationToken);
    Task DeletePlaceAsync(Guid structureId,Place place,CancellationToken cancellationToken);

    Task<IList<Place>> GetPlacesByStructureIdAsync(Guid structureId, CancellationToken cancellationToken);
    Task<IList<Structure>> GetStructuresByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    Task<Structure> GetStructureByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<Place> GetPlaceByIdAsync(Guid placeId, CancellationToken cancellationToken);
    Task<Structure> GetStructureByIdAsync(Guid structureId, CancellationToken cancellationToken);

    Task<Place> GetPlaceByIdAsyncAsNoTracking(Guid placeId, CancellationToken cancellationToken);
    Task<Structure> GetStructureByIdAsyncAsNoTracking(Guid structureId, CancellationToken cancellationToken);

    Task<IList<Structure>> GetAllStructuresByFilterAsync(CancellationToken cancellationToken, Expression<Func<Structure, bool>> filter = null);
    Task<IList<Place>> GetAllPlcaesByFilterAsync(CancellationToken cancellationToken, Expression<Func<Place, bool>> filter = null);
}
