using TechOnIt.Domain.Entities.Catalogs;

namespace TechOnIt.Infrastructure.Repositories.SQL.StructureAggregateRepository;

public class StructureRepository : IStructureRepository
{
    #region constructor
    private readonly IAppDbContext _context;

    public StructureRepository(IAppDbContext context)
    {
        _context = context;
    }
    #endregion

    #region Structure
    public async Task<StructureEntity?> FindByApiKeyAndPasswordAsync(Concurrency apiKey, PasswordHash password, CancellationToken cancellationToken)
        => await _context.Structures.AsNoTracking().Where(s => s.ApiKey.Value == apiKey.Value && s.Password.Value == password.Value).FirstOrDefaultAsync(cancellationToken);
    public async Task CreateAsync(StructureEntity structure, CancellationToken cancellationToken) => await _context.Structures.AddAsync(structure, cancellationToken);
    public async Task<bool> UpdateAsync(StructureEntity structure, CancellationToken cancellationToken)
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
    public async Task<StructureEntity?> GetByIdAsync(Guid structureId, CancellationToken cancellationToken)
        => await Task.FromResult(await _context.Structures.FirstOrDefaultAsync(a => a.Id == structureId, cancellationToken));
    public async Task<StructureEntity?> GetByIdAsyncAsNoTracking(Guid structureId, CancellationToken cancellationToken)
    => await Task.FromResult(await _context.Structures.AsNoTracking().FirstOrDefaultAsync(a => a.Id == structureId, cancellationToken));
    public async Task<IList<StructureEntity>> GetAllByFilterAsync(CancellationToken cancellationToken, Expression<Func<StructureEntity, bool>> filter = null)
    {
        IQueryable<StructureEntity> query = _context.Structures;
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
    public async Task CreateWithGroupsAsync(StructureEntity structure, IList<GroupEntity> groups, CancellationToken cancellationToken)
    {
        await _context.Structures.AddAsync(structure, cancellationToken);
        if (groups is not null)
            structure.AddRangeGroup(groups);

        await Task.CompletedTask;
    }
    public async Task<IList<GroupEntity>?> GetGroupsByStructureIdAsync(Guid structureId, CancellationToken cancellationToken)
    {
        var getstructure = await _context.Structures
            .Include(p => p.Groups)
            .FirstOrDefaultAsync(a => a.Id == structureId, cancellationToken);

        if (getstructure is not null)
            return getstructure.Groups as IList<GroupEntity>;
        else
            return await Task.FromResult<IList<GroupEntity>?>(null);
    }
    public async Task<GroupEntity?> GetGroupByStructureIdAsync(Guid structorId, Guid groupId, CancellationToken cancellationToken)
    {
        var group = await _context.Groups.FirstOrDefaultAsync(a => a.StructureId == structorId && a.Id == groupId, cancellationToken);
        if (group is null) return await Task.FromResult<GroupEntity?>(null);

        return await Task.FromResult(group);
    }
    #endregion

    #region Group
    public async Task<GroupEntity?> GetGroupByIdAsync(Guid groupId, CancellationToken cancellationToken)
        => await _context.Groups.FirstOrDefaultAsync(a => a.Id == groupId, cancellationToken);
    public async Task<GroupEntity?> GetGroupByIdAsyncAsNoTracking(Guid groupId, CancellationToken cancellationToken)
        => await Task.FromResult(await _context.Groups.AsNoTracking().FirstOrDefaultAsync(a => a.Id == groupId, cancellationToken));
    public async Task<IList<GroupEntity>> GetAllGroupsByFilterAsync(CancellationToken cancellationToken, Expression<Func<GroupEntity, bool>>? filter = null)
    {
        var groups = _context.Groups.AsNoTracking();
        if (filter != null) groups = groups.Where(filter);
        return await Task.FromResult(await groups.ToListAsync(cancellationToken));
    }
    public async Task CreateGroupAsync(GroupEntity group, Guid StructureId, CancellationToken cancellationToken)
    {
        await _context.Groups.AddAsync(group, cancellationToken);
        await Task.CompletedTask;
    }
    public async Task<bool> DeleteGroupAsync(Guid groupId, Guid structureId, CancellationToken cancellationToken)
    {
        GroupEntity? group = await _context.Groups.FirstOrDefaultAsync(a => a.StructureId == structureId && a.Id == groupId, cancellationToken);
        if (group is null) return false;

        _context.Groups.Remove(group);
        return true;
    }
    public async Task UpdateGroupAsync(Guid structureId, GroupEntity group, CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
            _context.Groups.Update(group);

        await Task.CompletedTask;
    }
    #endregion
}