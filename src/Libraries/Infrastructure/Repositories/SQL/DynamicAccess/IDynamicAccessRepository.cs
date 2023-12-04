namespace TechOnIt.Infrastructure.Repositories.SQL.DynamicAccess;

public interface IDynamicAccessRepository
{
    Task AddDynamicAccessAsync(TechOnIt.Domain.Entities.Identity.DynamicAccess model, CancellationToken cancellationToken);
    Task AddRangeDynamicAccessAsync(HashSet<TechOnIt.Domain.Entities.Identity.DynamicAccess> models, CancellationToken cancellationToken);
    Task DeleteFromDynamicAccessAsync(TechOnIt.Domain.Entities.Identity.DynamicAccess model, CancellationToken cancellationToken);
    Task DeleteRangeFromDynamicAccessAsync(HashSet<TechOnIt.Domain.Entities.Identity.DynamicAccess> models, CancellationToken cancellationToken);
    Task<HashSet<TechOnIt.Domain.Entities.Identity.DynamicAccess>> GetUserAccessAsync(Guid userId, CancellationToken cancellationToken);
}