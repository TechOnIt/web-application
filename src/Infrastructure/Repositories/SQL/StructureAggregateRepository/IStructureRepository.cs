using iot.Domain.Entities.Product.StructureAggregate;
using System.Linq.Expressions;

namespace iot.Application.Repositories.SQL.StructureAggregateRepository;

public interface IStructureRepository
{
    Task CreateStructureWithPlacesAsync(Structure structure,IList<Place> places,CancellationToken cancellationToken = default);
    Task CreateStructureAsync(Structure structure, CancellationToken cancellationToken = default);
    Task CreatePlaceAsync(Place place,Guid StructureId, CancellationToken cancellationToken = default);

    Task UpdatePlaceAsync(Guid structureId,Place place,CancellationToken cancellationToken = default);
    Task UpdateStructureAsync(Structure structure, CancellationToken cancellationToken = default);

    void DeleteStructure(Structure structure);
    Task DeleteStructureAsync(Guid structureId, CancellationToken cancellationToken = default);
    Task DeletePlaceAsync(Guid structureId,Place place,CancellationToken cancellationToken = default);

    Task<IList<Place>> GetPlacesByStructureIdAsync(Guid structureId, CancellationToken cancellationToken = default);
    Task<IList<Structure>> GetStructuresByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<Structure> GetStructureByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Place> GetPlaceByIdAsync(Guid placeId, CancellationToken cancellationToken = default);
    Task<Structure> GetStructureByIdAsync(Guid structureId, CancellationToken cancellationToken = default);

    Task<Place?> GetPlaceByIdAsyncAsNoTracking(Guid placeId, CancellationToken cancellationToken = default);
    Task<Structure?> GetStructureByIdAsyncAsNoTracking(Guid structureId, CancellationToken cancellationToken = default);

    Task<IList<Structure>> GetAllStructuresByFilterAsync(CancellationToken cancellationToken, Expression<Func<Structure, bool>> filter = null);
    Task<IList<Place>> GetAllPlcaesByFilterAsync(CancellationToken cancellationToken, Expression<Func<Place, bool>> filter = null);
}
