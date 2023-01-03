using TechOnIt.Domain.Entities.Product.StructureAggregate;
using System.Linq.Expressions;

namespace TechOnIt.Infrastructure.Repositories.SQL.StructureAggregateRepository;

public interface IStructureRepository
{
    #region Structure
    Task CreateAsync(Structure structure, CancellationToken cancellationToken = default);
    //Task UpdateAsync(Structure structure, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Structure structure, CancellationToken cancellationToken);
    Task DeleteAsync(Structure structure, CancellationToken cancellationToken);
    //Task DeleteByIdAsync(Guid structureId, CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAsync(Guid structureId, CancellationToken cancellationToken);
    Task<Structure> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Structure> GetByIdAsync(Guid structureId, CancellationToken cancellationToken = default);
    Task<Structure?> GetByIdAsyncAsNoTracking(Guid structureId, CancellationToken cancellationToken = default);
    Task<IList<Structure>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IList<Structure>> GetAllByFilterAsync(CancellationToken cancellationToken, Expression<Func<Structure, bool>> filter = null);
    Task<bool> IsExistsStructoreByIdAsync(Guid structoreId, CancellationToken cancellationToken);
    #endregion

    #region Common
    Task CreateWithPlacesAsync(Structure structure, IList<Place> places, CancellationToken cancellationToken = default);
    Task<IList<Place>?> GetPlacesByStructureIdAsync(Guid structureId, CancellationToken cancellationToken = default);
    Task<Place?> GetPlaceByStructureIdAsync(Guid structorId, Guid placeId, CancellationToken cancellationToken);
    #endregion

    #region Place
    Task<Place> GetPlaceByIdAsync(Guid placeId, CancellationToken cancellationToken = default);
    Task<Place?> GetPlaceByIdAsyncAsNoTracking(Guid placeId, CancellationToken cancellationToken = default);
    Task<IList<Place>> GetAllPlcaesByFilterAsync(CancellationToken cancellationToken, Expression<Func<Place, bool>> filter = null);
    Task CreatePlaceAsync(Place place, Guid StructureId, CancellationToken cancellationToken = default);
    Task UpdatePlaceAsync(Guid structureId, Place place, CancellationToken cancellationToken = default);
    Task<bool> DeletePlaceAsync(Guid placeId, Guid structureId, CancellationToken cancellationToken);
    #endregion
}
