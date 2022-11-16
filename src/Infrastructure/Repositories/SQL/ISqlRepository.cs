using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace iot.Infrastructure.Repositories.SQL;

public interface ISqlRepository<TEntity> where TEntity : class
{
    DbSet<TEntity> Entities { get; }
    IQueryable<TEntity> Table { get; }
    IQueryable<TEntity> TableNoTracking { get; }

    TEntity? GetById(params object[] id);
    Task<TEntity?> GetByIdAsync(CancellationToken stoppingToken = default, params object[] id);
    List<TEntity> GetAll(bool asNoTracking);
    Task<List<TEntity>> GetAllAsync(bool asNoTracking, CancellationToken stoppingToken = default);

    void Add(TEntity entity);
    Task AddAsync(TEntity entity, CancellationToken stoppingToken = default);
    void AddRange(IEnumerable<TEntity> entities);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken stoppingToken = default);

    void Update(TEntity entity);
    Task UpdateAsync(TEntity entity, CancellationToken stoppingToken = default);
    void UpdateRange(IEnumerable<TEntity> entities);
    Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken stoppingToken = default);

    void Delete(TEntity entity);
    Task DeleteAsync(TEntity entity, CancellationToken stoppingToken = default);
    void DeleteRange(IEnumerable<TEntity> entities);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken stoppingToken = default);

    bool IsExists(Expression<Func<TEntity, bool>> expression);
    Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken stoppingToken = default);
}