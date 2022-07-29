using iot.Application.Common.Extentions;
using iot.Application.Common.Interfaces.Context;
using iot.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace iot.Application.Repositories.SQL;

public class SqlRepository<TEntity> : ISqlRepository<TEntity>
        where TEntity : class, IEntity
{
    protected readonly IIdentityContext _context;
    public DbSet<TEntity> Entities { get; }
    public virtual IQueryable<TEntity> Table => Entities;
    public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

    public SqlRepository(IIdentityContext context)
    {
        _context = context;
        Entities = _context.Set<TEntity>();
    }

    #region Async Method
    public async virtual Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
    {
        return await Entities.FindAsync(ids, cancellationToken);
    }

    public virtual async Task AddAsync(TEntity entity, bool saveNow = true, CancellationToken cancellationToken = default)
    {
        Assert.NotNull(entity, nameof(entity));
        await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        if (saveNow)
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
    {
        Assert.NotNull(entities, nameof(entities));
        await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
        if (saveNow)
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));
        Entities.Update(entity);
        if (saveNow)
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
    {
        Assert.NotNull(entities, nameof(entities));
        Entities.UpdateRange(entities);
        if (saveNow)
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));
        Entities.Remove(entity);
        if (saveNow)
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
    {
        Assert.NotNull(entities, nameof(entities));
        Entities.RemoveRange(entities);
        if (saveNow)
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
    #endregion

    #region Sync Methods
    public virtual TEntity GetById(params object[] ids)
    {
        return Entities.Find(ids);
    }

    public virtual void Add(TEntity entity, bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));
        Entities.Add(entity);
        if (saveNow)
            _context.SaveChanges();
    }

    public virtual void AddRange(IEnumerable<TEntity> entities, bool saveNow = true)
    {
        Assert.NotNull(entities, nameof(entities));
        Entities.AddRange(entities);
        if (saveNow)
            _context.SaveChanges();
    }

    public virtual void Update(TEntity entity, bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));
        Entities.Update(entity);
        if (saveNow)
            _context.SaveChanges();
    }

    public virtual void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true)
    {
        Assert.NotNull(entities, nameof(entities));
        Entities.UpdateRange(entities);
        if (saveNow)
            _context.SaveChanges();
    }

    public virtual void Delete(TEntity entity, bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));
        Entities.Remove(entity);
        if (saveNow)
            _context.SaveChanges();
    }

    public virtual void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true)
    {
        Assert.NotNull(entities, nameof(entities));
        Entities.RemoveRange(entities);
        if (saveNow)
            _context.SaveChanges();
    }

    public virtual async Task<List<TEntity>> GetAllAsync(bool asNoTracking, CancellationToken cancellationToken)
    {
        return await (asNoTracking ? TableNoTracking : Entities)
            .ToListAsync(cancellationToken);
    }

    #endregion

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await Entities.AnyAsync(expression);
    }
}