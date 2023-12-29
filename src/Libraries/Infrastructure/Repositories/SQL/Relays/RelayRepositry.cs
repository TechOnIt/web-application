using Microsoft.EntityFrameworkCore;
using TechOnIt.Domain.Entities;
using TechOnIt.Infrastructure.Persistence.Context;

namespace TechOnIt.Infrastructure.Repositories.SQL.Relays;

public class RelayRepositry : IRelayRepositry
{
    #region Ctor
    private readonly IdentityContext _context;

    public RelayRepositry(IdentityContext context)
    {
        _context = context;
    }
    #endregion

    public async Task<RelayEntity?> FindByIdAsync(Guid relayId, CancellationToken cancellationToken = default)
    {
        var getRelay = await _context.Relays.FirstOrDefaultAsync(a => a.Id == relayId, cancellationToken);
        if (getRelay is null)
        {
            await Task.CompletedTask;
            return await Task.FromResult<RelayEntity?>(null);
        }

        return await Task.FromResult(getRelay);
    }
    public async Task<RelayEntity?> FindByIdAsNoTrackingAsync(Guid relayId, CancellationToken cancellationToken = default)
    {
        var getRelay = await _context.Relays
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == relayId, cancellationToken);

        if (getRelay is null)
            return await Task.FromResult<RelayEntity?>(null);

        return await Task.FromResult<RelayEntity?>(getRelay);
    }
    /// <summary>
    /// Create a new relay.
    /// </summary>
    /// <param name="relay">Relay model object.</param>
    public async Task CreateAsync(RelayEntity relay, CancellationToken cancellationToken = default)
    {
        var result = await _context.Relays.AddAsync(relay, cancellationToken);
        await Task.CompletedTask;
    }
    public async Task UpdateAsync(RelayEntity relay, CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
            _context.Relays.Update(relay);

        await Task.CompletedTask;
    }
    /// <summary>
    /// Delete an specific relay by id.
    /// </summary>
    /// <param name="RelayId"></param>
    /// <returns></returns>
    public async Task DeleteByIdAsync(Guid RelayId, CancellationToken cancellationToken = default)
    {
        var getRelay = await _context.Relays.FirstOrDefaultAsync(a => a.Id == RelayId);

        if (getRelay is null)
            await Task.CompletedTask;

        if (!cancellationToken.IsCancellationRequested)
            _context.Entry<RelayEntity>(getRelay).State = EntityState.Deleted;

        await Task.CompletedTask;
    }
    public async Task DeleteAsync(RelayEntity relay, CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            _context.Relays.Remove(relay);
        }

        await Task.CompletedTask;
    }

    #region bad method
    public async Task<IList<RelayEntity>> GetAllRelaysAsync(CancellationToken cancellationToken, Func<RelayEntity, bool>? filter = null)
    {
        var relays = _context.Relays.AsNoTracking().AsQueryable();

        if (filter is null)
            return await relays.ToListAsync();

        return await Task.FromResult(relays.Where(filter).ToList());
    }
    #endregion

}