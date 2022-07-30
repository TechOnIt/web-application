using iot.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace iot.Application.Repositories.SQL;

public interface ISqlRepository<TEntity> where TEntity : class, IEntity
{
    DbSet<TEntity> Entities { get; }
    IQueryable<TEntity> Table { get; }
    IQueryable<TEntity> TableNoTracking { get; }

    TEntity? GetById(params object[] id);
    Task<TEntity?> GetByIdAsync(CancellationToken stoppingToken = default, params object[] id);
    List<TEntity> GetAll(bool asNoTracking);
    Task<List<TEntity>> GetAllAsync(bool asNoTracking, CancellationToken stoppingToken = default);

    void Add(TEntity entity, bool saveNow = true);
    Task AddAsync(TEntity entity, bool saveNow = true, CancellationToken stoppingToken = default);
    void AddRange(IEnumerable<TEntity> entities, bool saveNow = true);
    Task AddRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true, CancellationToken stoppingToken = default);

    void Update(TEntity entity, bool saveNow = true);
    Task UpdateAsync(TEntity entity, bool saveNow = true, CancellationToken stoppingToken = default);
    void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true);
    Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true, CancellationToken stoppingToken = default);

    void Delete(TEntity entity, bool saveNow = true);
    Task DeleteAsync(TEntity entity, bool saveNow = true, CancellationToken stoppingToken = default);
    void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true, CancellationToken stoppingToken = default);

    bool IsExists(Expression<Func<TEntity, bool>> expression);
    Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken stoppingToken = default);
}