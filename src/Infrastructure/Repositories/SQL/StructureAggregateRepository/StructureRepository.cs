using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechOnIt.Infrastructure.Persistence.Context;
using TechOnIt.Domain.ValueObjects;
using TechOnIt.Domain.Entities.StructureAggregate;

namespace TechOnIt.Infrastructure.Repositories.SQL.StructureAggregateRepository;

public class StructureRepository : IStructureRepository
{
    #region constructor
    private readonly IdentityContext _context;

    public StructureRepository(IdentityContext context)
    {
        _context = context;
    }
    #endregion

    #region Structure
    public async Task<Structure?> FindByApiKeyNoTrackingAsync(Concurrency apiKey, CancellationToken cancellationToken)
        => await _context.Structures
        .Where(structure => structure.ApiKey.Value == apiKey.Value)
        .AsNoTracking()
        .FirstOrDefaultAsync(cancellationToken);
    public async Task CreateAsync(Structure structure, CancellationToken cancellationToken) => await _context.Structures.AddAsync(structure, cancellationToken);
    public async Task<bool> UpdateAsync(Structure structure, CancellationToken cancellationToken)
    {
        var getStructure = await _context.Structures
            .FirstOrDefaultAsync(a => a.Id == structure.Id, cancellationToken);

        if (getStructure is not null)
            return false;

        getStructure.Description = structure.Description;
        getStructure.SetStructureType(structure.Type);
        getStructure.IsActive = structure.IsActive;
        getStructure.Name = structure.Name;
        getStructure.SetModifyDate();

        cancellationToken.ThrowIfCancellationRequested();
        _context.Structures.Update(getStructure);
        return true;
    }
    public async Task DeleteAsync(Structure structure, CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
            _context.Structures.Remove(structure);

        await Task.CompletedTask;
    }
    public async Task<bool> DeleteByIdAsync(Guid structureId, CancellationToken cancellationToken)
    {
        //https://entityframework-extensions.net/delete-from-query
        //var getStructure = await _context.Structures.Where(a => a.Id == structureId).DeleteFromQuery(); .net7 - efcore 7

        var getStructure = await _context.Structures.FirstOrDefaultAsync(a => a.Id == structureId, cancellationToken);
        if (getStructure is not null)
        {
            _context.Structures.Remove(getStructure);
            return true;
        }
        else
            return false;
    }
    public async Task<Structure> GetByIdAsync(Guid structureId, CancellationToken cancellationToken)
        => await Task.FromResult(await _context.Structures.FirstOrDefaultAsync(a => a.Id == structureId, cancellationToken) ?? new Structure());
    public async Task<Structure> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    => await Task.FromResult(await _context.Structures.FirstOrDefaultAsync(a => a.UserId == userId, cancellationToken) ?? new Structure());
    public async Task<Structure?> GetByIdAsyncAsNoTracking(Guid structureId, CancellationToken cancellationToken)
    => await Task.FromResult(await _context.Structures.AsNoTracking().FirstOrDefaultAsync(a => a.Id == structureId, cancellationToken));
    public async Task<IList<Structure>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var getstructures = await _context.Structures.Where(a => a.UserId == userId).AsNoTracking().ToListAsync(cancellationToken);

        if (getstructures.Any()) return await Task.FromResult(getstructures);
        else return await Task.FromResult(new List<Structure>());
    }
    public async Task<IList<Structure>> GetAllByFilterAsync(CancellationToken cancellationToken, Expression<Func<Structure, bool>> filter = null)
    {
        IQueryable<Structure> query = _context.Structures;
        if (query.Count() > 1000)
        {
            query = _context.Structures.AsNoTracking().AsParallel().WithDegreeOfParallelism(4).AsQueryable();
            if (filter != null) query = query.Where(filter);
            return await Task.FromResult(await query.ToListAsync(cancellationToken));
        }
        else
        {
            query = _context.Structures.AsNoTracking();
            if (filter != null) query = query.Where(filter);
            return await Task.FromResult(await query.ToListAsync(cancellationToken));
        }
    }
    public async Task<bool> IsExistsStructoreByIdAsync(Guid structoreId, CancellationToken cancellationToken)
        => await Task.FromResult(await _context.Structures.AnyAsync(a => a.Id == structoreId));
    #endregion

    #region Common
    public async Task CreateWithPlacesAsync(Structure structure, IList<Place> places, CancellationToken cancellationToken)
    {
        await _context.Structures.AddAsync(structure, cancellationToken);
        if (places is not null)
            structure.AddRangePlace(places);

        await Task.CompletedTask;
    }

    public async Task<IList<Place>?> GetPlacesByStructureIdAsync(Guid structureId, CancellationToken cancellationToken)
    {
        var getstructure = await _context.Structures
            .Include(p => p.Places)
            .FirstOrDefaultAsync(a => a.Id == structureId, cancellationToken);

        if (getstructure is not null)
            return getstructure.Places as IList<Place>;
        else
            return await Task.FromResult<IList<Place>?>(null);
    }

    public async Task<Place?> GetPlaceByStructureIdAsync(Guid structorId, Guid placeId, CancellationToken cancellationToken)
    {
        var place = await _context.Places.FirstOrDefaultAsync(a => a.StuctureId == structorId && a.Id == placeId, cancellationToken);
        if (place is null) return await Task.FromResult<Place?>(null);

        return await Task.FromResult(place);
    }

    #endregion

    #region Place
    public async Task<Place> GetPlaceByIdAsync(Guid placeId, CancellationToken cancellationToken)
        => await _context.Places.FirstOrDefaultAsync(a => a.Id == placeId, cancellationToken) ?? new Place();
    public async Task<Place?> GetPlaceByIdAsyncAsNoTracking(Guid placeId, CancellationToken cancellationToken)
        => await Task.FromResult(await _context.Places.AsNoTracking().FirstOrDefaultAsync(a => a.Id == placeId, cancellationToken));
    public async Task<IList<Place>> GetAllPlcaesByFilterAsync(CancellationToken cancellationToken, Expression<Func<Place, bool>>? filter = null)
    {
        var places = _context.Places.AsNoTracking();
        if (filter != null) places = places.Where(filter);
        return await Task.FromResult(await places.ToListAsync(cancellationToken));
    }
    public async Task CreatePlaceAsync(Place place, Guid StructureId, CancellationToken cancellationToken)
    {
        await _context.Places.AddAsync(place, cancellationToken);
        await Task.CompletedTask;
    }
    public async Task<bool> DeletePlaceAsync(Guid placeId, Guid structureId, CancellationToken cancellationToken)
    {
        Place? place = await _context.Places.FirstOrDefaultAsync(a => a.StuctureId == structureId && a.Id == placeId, cancellationToken);
        if (place is null) return false;

        _context.Places.Remove(place);
        return true;
    }
    public async Task UpdatePlaceAsync(Guid structureId, Place place, CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
            _context.Places.Update(place);

        await Task.CompletedTask;
    }
    #endregion
}