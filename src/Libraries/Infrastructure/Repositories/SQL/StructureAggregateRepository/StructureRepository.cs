using System.Linq.Expressions;
using TechOnIt.Domain.Entities.Catalog;
using TechOnIt.Domain.ValueObjects;
using TechOnIt.Infrastructure.Persistence.Context;

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
    public async Task<Structure?> FindByApiKeyAndPasswordAsync(Concurrency apiKey, PasswordHash password, CancellationToken cancellationToken)
        => await _context.Structures.AsNoTracking().Where(s => s.ApiKey.Value == apiKey.Value && s.Password.Value == password.Value).FirstOrDefaultAsync(cancellationToken);
    public async Task CreateAsync(Structure structure, CancellationToken cancellationToken) => await _context.Structures.AddAsync(structure, cancellationToken);
    public async Task<bool> UpdateAsync(Structure structure, CancellationToken cancellationToken)
    {
        var getStructure = await _context.Structures
            .FirstOrDefaultAsync(a => a.Id == structure.Id, cancellationToken);

        if (getStructure is null)
            return false;

        getStructure.Description = structure.Description;
        getStructure.SetName(structure.Name);

        cancellationToken.ThrowIfCancellationRequested();
        _context.Structures.Update(getStructure);
        return true;
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
    public async Task<Structure?> GetByIdAsync(Guid structureId, CancellationToken cancellationToken)
        => await Task.FromResult(await _context.Structures.FirstOrDefaultAsync(a => a.Id == structureId, cancellationToken));
    public async Task<Structure?> GetByIdAsyncAsNoTracking(Guid structureId, CancellationToken cancellationToken)
    => await Task.FromResult(await _context.Structures.AsNoTracking().FirstOrDefaultAsync(a => a.Id == structureId, cancellationToken));
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
    #endregion

    #region Common
    public async Task CreateWithGroupsAsync(Structure structure, IList<Group> groups, CancellationToken cancellationToken)
    {
        await _context.Structures.AddAsync(structure, cancellationToken);
        if (groups is not null)
            structure.AddRangeGroup(groups);

        await Task.CompletedTask;
    }
    public async Task<IList<Group>?> GetGroupsByStructureIdAsync(Guid structureId, CancellationToken cancellationToken)
    {
        var getstructure = await _context.Structures
            .Include(p => p.Groups)
            .FirstOrDefaultAsync(a => a.Id == structureId, cancellationToken);

        if (getstructure is not null)
            return getstructure.Groups as IList<Group>;
        else
            return await Task.FromResult<IList<Group>?>(null);
    }
    public async Task<Group?> GetGroupByStructureIdAsync(Guid structorId, Guid groupId, CancellationToken cancellationToken)
    {
        var group = await _context.Groups.FirstOrDefaultAsync(a => a.StructureId == structorId && a.Id == groupId, cancellationToken);
        if (group is null) return await Task.FromResult<Group?>(null);

        return await Task.FromResult(group);
    }
    #endregion

    #region Group
    public async Task<Group?> GetGroupByIdAsync(Guid groupId, CancellationToken cancellationToken)
        => await _context.Groups.FirstOrDefaultAsync(a => a.Id == groupId, cancellationToken);
    public async Task<Group?> GetGroupByIdAsyncAsNoTracking(Guid groupId, CancellationToken cancellationToken)
        => await Task.FromResult(await _context.Groups.AsNoTracking().FirstOrDefaultAsync(a => a.Id == groupId, cancellationToken));
    public async Task<IList<Group>> GetAllGroupsByFilterAsync(CancellationToken cancellationToken, Expression<Func<Group, bool>>? filter = null)
    {
        var groups = _context.Groups.AsNoTracking();
        if (filter != null) groups = groups.Where(filter);
        return await Task.FromResult(await groups.ToListAsync(cancellationToken));
    }
    public async Task CreateGroupAsync(Group group, Guid StructureId, CancellationToken cancellationToken)
    {
        await _context.Groups.AddAsync(group, cancellationToken);
        await Task.CompletedTask;
    }
    public async Task<bool> DeleteGroupAsync(Guid groupId, Guid structureId, CancellationToken cancellationToken)
    {
        Group? group = await _context.Groups.FirstOrDefaultAsync(a => a.StructureId == structureId && a.Id == groupId, cancellationToken);
        if (group is null) return false;

        _context.Groups.Remove(group);
        return true;
    }
    public async Task UpdateGroupAsync(Guid structureId, Group group, CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
            _context.Groups.Update(group);

        await Task.CompletedTask;
    }
    #endregion
}