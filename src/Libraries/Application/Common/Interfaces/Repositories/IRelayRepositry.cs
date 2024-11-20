using TechOnIt.Domain.Entities.Controllers;

namespace TechOnIt.Application.Common.Interfaces.Repositories;

public interface IRelayRepositry
{
    Task<RelayEntity> FindByIdAsync(Guid relayId, CancellationToken cancellationToken = default);
    Task<RelayEntity> FindByIdAsNoTrackingAsync(Guid relayId, CancellationToken cancellationToken = default);
    Task CreateAsync(RelayEntity relay, CancellationToken cancellationToken = default);
    Task UpdateAsync(RelayEntity relay, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(Guid RelayId, CancellationToken cancellationToken = default);
    Task DeleteAsync(RelayEntity relay, CancellationToken cancellationToken);
    Task<IList<RelayEntity>> GetAllRelaysAsync(CancellationToken cancellationToken, Func<RelayEntity, bool> filter = null);
}
