using TechOnIt.Application.Common.Interfaces.Repositories;
using TechOnIt.Domain.Entities.Securities;
using TechOnIt.Infrastructure.Persistence.Context;

namespace TechOnIt.Infrastructure.Repositories.SQL.DynamicAccess;

public class DynamicAccessRepository : IDynamicAccessRepository
{
    private readonly IAppDbContext _context;
    public DynamicAccessRepository(IAppDbContext context)
    {
        _context = context;
    }

    public async Task AddDynamicAccessAsync(DynamicAccessEntity model, CancellationToken cancellationToken)
        => await _context.DynamicAccesses.AddAsync(model, cancellationToken);

    public async Task AddRangeDynamicAccessAsync(HashSet<DynamicAccessEntity> models, CancellationToken cancellationToken)
    => await _context.DynamicAccesses.AddRangeAsync(models, cancellationToken);


    public async Task DeleteFromDynamicAccessAsync(DynamicAccessEntity model, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _context.DynamicAccesses.Remove(model);
        await Task.CompletedTask;
    }

    public async Task DeleteRangeFromDynamicAccessAsync(HashSet<DynamicAccessEntity> models, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _context.DynamicAccesses.RemoveRange(models);
        await Task.CompletedTask;
    }

    public async Task<HashSet<DynamicAccessEntity>> GetUserAccessAsync(Guid userId,CancellationToken cancellationToken)
    {
        HashSet<DynamicAccessEntity> allUseraccessPath = new HashSet<DynamicAccessEntity>();
        var getUserPath = await _context.DynamicAccesses.Where(a => a.UserId == userId).ToListAsync(cancellationToken);
        allUseraccessPath = getUserPath.ToHashSet();

        return allUseraccessPath;
    }
}
