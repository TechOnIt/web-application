using TechOnIt.Infrastructure.Persistence.Context;

namespace TechOnIt.Infrastructure.Repositories.SQL.DynamicAccess;

public class DynamicAccessRepository : IDynamicAccessRepository
{
    private readonly IdentityContext _context;
    public DynamicAccessRepository(IdentityContext context)
    {
        _context = context;
    }

    public async Task AddDynamicAccessAsync(TechOnIt.Domain.Entities.Identity.DynamicAccessEntity model, CancellationToken cancellationToken)
        => await _context.DynamicAccesses.AddAsync(model, cancellationToken);

    public async Task AddRangeDynamicAccessAsync(HashSet<TechOnIt.Domain.Entities.Identity.DynamicAccessEntity> models, CancellationToken cancellationToken)
    => await _context.DynamicAccesses.AddRangeAsync(models, cancellationToken);


    public async Task DeleteFromDynamicAccessAsync(TechOnIt.Domain.Entities.Identity.DynamicAccessEntity model, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _context.DynamicAccesses.Remove(model);
        await Task.CompletedTask;
    }

    public async Task DeleteRangeFromDynamicAccessAsync(HashSet<TechOnIt.Domain.Entities.Identity.DynamicAccessEntity> models, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _context.DynamicAccesses.RemoveRange(models);
        await Task.CompletedTask;
    }

    public async Task<HashSet<TechOnIt.Domain.Entities.Identity.DynamicAccessEntity>> GetUserAccessAsync(Guid userId,CancellationToken cancellationToken)
    {
        HashSet<TechOnIt.Domain.Entities.Identity.DynamicAccessEntity> allUseraccessPath = new HashSet<TechOnIt.Domain.Entities.Identity.DynamicAccessEntity>();
        var getUserPath = await _context.DynamicAccesses.Where(a => a.UserId == userId).ToListAsync(cancellationToken);
        allUseraccessPath = getUserPath.ToHashSet();

        return allUseraccessPath;
    }
}
