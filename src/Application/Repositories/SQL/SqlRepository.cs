using iot.Application.Common.Extentions;
using System.Linq.Expressions;

namespace iot.Application.Repositories.SQL;

public class SqlRepository<TEntity,TContext> : ISqlRepository<TEntity>
        where TEntity : class where TContext : DbContext
{
    protected readonly TContext _context;
    public DbSet<TEntity> Entities { get; }
    public virtual IQueryable<TEntity> Table => Entities;
    public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

    public SqlRepository(TContext context)
    {
        _context = context;
        Entities = _context.Set<TEntity>();
    }

    #region Sync Methods
    public virtual TEntity? GetById(params object[] id) => Entities.Find(id);
    public virtual List<TEntity> GetAll(bool asNoTracking) => (asNoTracking ? TableNoTracking : Entities).ToList();

    public virtual bool Add(TEntity entity, bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));
        Entities.Add(entity);
        if (saveNow)
            return Convert.ToBoolean(_context.SaveChanges());
        return false;
    }

    public virtual bool AddRange(IEnumerable<TEntity> entities, bool saveNow = true)
    {
        Assert.NotNull(entities, nameof(entities));
        Entities.AddRange(entities);
        if (saveNow)
            return Convert.ToBoolean(_context.SaveChanges());
        return false;
    }

    public virtual bool Update(TEntity entity, bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));
        Entities.Update(entity);
        if (saveNow)
            return Convert.ToBoolean(_context.SaveChanges());
        return false;
    }

    public virtual bool UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true)
    {
        Assert.NotNull(entities, nameof(entities));
        Entities.UpdateRange(entities);
        if (saveNow)
            return Convert.ToBoolean(_context.SaveChanges());
        return false;
    }

    public virtual bool Delete(TEntity entity, bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));
        Entities.Remove(entity);
        if (saveNow)
            return Convert.ToBoolean(_context.SaveChanges());
        return false;
    }

    public virtual bool DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true)
    {
        Assert.NotNull(entities, nameof(entities));
        Entities.RemoveRange(entities);
        if (saveNow)
            return Convert.ToBoolean(_context.SaveChanges());
        return false;
    }

    public bool IsExists(Expression<Func<TEntity, bool>> expression)
    {
        return Entities.Any(expression);
    }

    #endregion

    #region Async Method
    public async virtual Task<TEntity?> GetByIdAsync(CancellationToken cancellationToken, params object[] ids) =>
        await Entities.FindAsync(ids, cancellationToken);
    public virtual async Task<List<TEntity>> GetAllAsync(bool asNoTracking, CancellationToken cancellationToken = default) =>
        await (asNoTracking ? TableNoTracking : Entities).ToListAsync(cancellationToken);

    public virtual async Task<bool> AddAsync(TEntity entity, bool saveNow = true, CancellationToken cancellationToken = default)
    {
        Assert.NotNull(entity, nameof(entity));
        await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        if (saveNow)
            return Convert.ToBoolean(await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false));
        return false;
    }
    public virtual async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true, CancellationToken cancellationToken = default)
    {
        Assert.NotNull(entities, nameof(entities));
        await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
        if (saveNow)
            return Convert.ToBoolean(await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false));
        return false;
    }

    public virtual async Task<bool> UpdateAsync(TEntity entity, bool saveNow = true, CancellationToken cancellationToken = default)
    {
        Assert.NotNull(entity, nameof(entity));
        Entities.Update(entity);
        if (saveNow)
            return Convert.ToBoolean(await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false));
        return false;
    }
    public virtual async Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true, CancellationToken cancellationToken = default)
    {
        Assert.NotNull(entities, nameof(entities));
        Entities.UpdateRange(entities);
        if (saveNow)
            return Convert.ToBoolean(await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false));
        return false;
    }

    public virtual async Task<bool> DeleteAsync(TEntity entity, bool saveNow = true, CancellationToken cancellationToken = default)
    {
        Assert.NotNull(entity, nameof(entity));
        Entities.Remove(entity);
        if (saveNow)
            return Convert.ToBoolean(await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false));
        return false;
    }
    public virtual async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true, CancellationToken cancellationToken = default)
    {
        Assert.NotNull(entities, nameof(entities));
        Entities.RemoveRange(entities);
        if (saveNow)
            return Convert.ToBoolean(await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false));
        return false;
    }

    public async Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken stoppingToken = default)
    {
        return await Entities.AnyAsync(expression, stoppingToken);
    }
    #endregion
}