using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace iot.Infrastructure.Persistence.Context;

public interface IContext
{
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    public DatabaseFacade Database { get; }
}