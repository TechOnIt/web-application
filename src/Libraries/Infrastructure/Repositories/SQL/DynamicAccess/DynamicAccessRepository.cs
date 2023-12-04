using TechOnIt.Infrastructure.Persistence.Context;

namespace TechOnIt.Infrastructure.Repositories.SQL.DynamicAccess;

public class DynamicAccessRepository : IDynamicAccessRepository
{
    private readonly IdentityContext _context;
    public DynamicAccessRepository(IdentityContext context)
    {
        _context = context;
    }

    public async Task AddDynamicAccessAsync(TechOnIt.Domain.Entities.Identity.DynamicAccess model, CancellationToken cancellationToken)
        => await _context.DynamicAccesses.AddAsync(model, cancellationToken);

    public async Task AddRangeDynamicAccessAsync(HashSet<TechOnIt.Domain.Entities.Identity.DynamicAccess> models, CancellationToken cancellationToken)
    => await _context.DynamicAccesses.AddRangeAsync(models, cancellationToken);


    public async Task DeleteFromDynamicAccessAsync(TechOnIt.Domain.Entities.Identity.DynamicAccess model, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _context.DynamicAccesses.Remove(model);
        await Task.CompletedTask;
    }

    public async Task DeleteRangeFromDynamicAccessAsync(HashSet<TechOnIt.Domain.Entities.Identity.DynamicAccess> models, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _context.DynamicAccesses.RemoveRange(models);
        await Task.CompletedTask;
    }

    public async Task<HashSet<TechOnIt.Domain.Entities.Identity.DynamicAccess>> GetUserAccessAsync(Guid userId,CancellationToken cancellationToken)
    {
        HashSet<TechOnIt.Domain.Entities.Identity.DynamicAccess> allUseraccessPath = new HashSet<TechOnIt.Domain.Entities.Identity.DynamicAccess>();
        var getUserPath = await _context.DynamicAccesses.Where(a => a.UserId == userId).ToListAsync(cancellationToken);
        allUseraccessPath = getUserPath.ToHashSet();

        return allUseraccessPath;
    }
}
