using TechOnIt.Domain.Entities.Security;

namespace TechOnIt.Infrastructure.Repositories.SQL.DynamicAccess;

public interface IDynamicAccessRepository
{
    Task AddDynamicAccessAsync(DynamicAccessEntity model, CancellationToken cancellationToken);
    Task AddRangeDynamicAccessAsync(HashSet<DynamicAccessEntity> models, CancellationToken cancellationToken);
    Task DeleteFromDynamicAccessAsync(DynamicAccessEntity model, CancellationToken cancellationToken);
    Task DeleteRangeFromDynamicAccessAsync(HashSet<DynamicAccessEntity> models, CancellationToken cancellationToken);
    Task<HashSet<DynamicAccessEntity>> GetUserAccessAsync(Guid userId, CancellationToken cancellationToken);
}