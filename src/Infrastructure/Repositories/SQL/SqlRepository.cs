using iot.Application.Common.Extentions;
using iot.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace iot.Application.Repositories.SQL;

public class SqlRepository<TEntity, TContext> : ISqlRepository<TEntity>
        where TEntity : class where TContext : IContext
{
    #region DI & Ctor
    protected readonly TContext _context;
    public DbSet<TEntity> Entities { get; }
    public virtual IQueryable<TEntity> Table => Entities;
    public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

    public SqlRepository(TContext context)
    {
        _context = context;
        Entities = _context.Set<TEntity>();
    }
    #endregion

    #region Sync Methods
    public virtual TEntity? GetById(params object[] id) => Entities.Find(id);
    public virtual List<TEntity> GetAll(bool asNoTracking) => (asNoTracking ? TableNoTracking : Entities).ToList();
    public bool IsExists(Expression<Func<TEntity, bool>> expression)
    {
        return Entities.Any(expression);
    }

    public virtual void Add(TEntity entity)
    {
        Assert.NotNull(entity, nameof(entity));
        Entities.Add(entity);
    }
    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        Assert.NotNull(entities, nameof(entities));
        Entities.AddRange(entities);
    }
    public virtual void Update(TEntity entity)
    {
        Assert.NotNull(entity, nameof(entity));
        Entities.Update(entity);
    }
    public virtual void UpdateRange(IEnumerable<TEntity> entities)
    {
        Assert.NotNull(entities, nameof(entities));
        Entities.UpdateRange(entities);
    }
    public virtual void Delete(TEntity entity)
    {
        Assert.NotNull(entity, nameof(entity));
        Entities.Remove(entity);
    }
    public virtual void DeleteRange(IEnumerable<TEntity> entities)
    {
        Assert.NotNull(entities, nameof(entities));
        Entities.RemoveRange(entities);
    }
    #endregion

    #region Async Method
    public async virtual Task<TEntity?> GetByIdAsync(CancellationToken cancellationToken, params object[] ids) =>
        await Entities.FindAsync(ids, cancellationToken);
    public virtual async Task<List<TEntity>> GetAllAsync(bool asNoTracking, CancellationToken cancellationToken = default) =>
        await (asNoTracking ? TableNoTracking : Entities).ToListAsync(cancellationToken);
    public async Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken stoppingToken = default)
    {
        return await Entities.AsNoTracking().AnyAsync(expression, stoppingToken);
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Assert.NotNull(entity, nameof(entity));
        await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
    }
    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        Assert.NotNull(entities, nameof(entities));
        await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        // TODO:
        // Make async task.
        Assert.NotNull(entity, nameof(entity));
        Entities.Update(entity);
    }
    public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        // TODO:
        // Make async task.
        Assert.NotNull(entities, nameof(entities));
        Entities.UpdateRange(entities);
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        // TODO:
        // Make async task.
        Assert.NotNull(entity, nameof(entity));
        Entities.Remove(entity);
    }
    public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        // TODO:
        // Make async task.
        Assert.NotNull(entities, nameof(entities));
        Entities.RemoveRange(entities);
    }
    #endregion
}