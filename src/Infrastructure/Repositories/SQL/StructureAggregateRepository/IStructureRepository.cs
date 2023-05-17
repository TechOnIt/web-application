using System.Linq.Expressions;
using TechOnIt.Domain.Entities.StructureAggregate;
using TechOnIt.Domain.ValueObjects;

namespace TechOnIt.Infrastructure.Repositories.SQL.StructureAggregateRepository;

public interface IStructureRepository
{
    #region Structure
    Task<Structure?> FindByApiKeyAndPasswordAsync(Concurrency apiKey, PasswordHash password, CancellationToken cancellationToken);
    Task CreateAsync(Structure structure, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Structure structure, CancellationToken cancellationToken);
    Task<bool> DeleteByIdAsync(Guid structureId, CancellationToken cancellationToken);
    Task<Structure?> GetByIdAsync(Guid structureId, CancellationToken cancellationToken = default);
    Task<Structure?> GetByIdAsyncAsNoTracking(Guid structureId, CancellationToken cancellationToken = default);
    Task<IList<Structure>> GetAllByFilterAsync(CancellationToken cancellationToken, Expression<Func<Structure, bool>> filter = null);
    #endregion

    #region Common
    Task CreateWithPlacesAsync(Structure structure, IList<Place> places, CancellationToken cancellationToken = default);
    Task<IList<Place>?> GetPlacesByStructureIdAsync(Guid structureId, CancellationToken cancellationToken = default);
    Task<Place?> GetPlaceByStructureIdAsync(Guid structorId, Guid placeId, CancellationToken cancellationToken);
    #endregion

    #region Place
    Task<Place?> GetPlaceByIdAsync(Guid placeId, CancellationToken cancellationToken = default);
    Task<Place?> GetPlaceByIdAsyncAsNoTracking(Guid placeId, CancellationToken cancellationToken = default);
    Task<IList<Place>> GetAllPlcaesByFilterAsync(CancellationToken cancellationToken, Expression<Func<Place, bool>> filter = null);
    Task CreatePlaceAsync(Place place, Guid StructureId, CancellationToken cancellationToken = default);
    Task UpdatePlaceAsync(Guid structureId, Place place, CancellationToken cancellationToken = default);
    Task<bool> DeletePlaceAsync(Guid placeId, Guid structureId, CancellationToken cancellationToken);
    #endregion
}
