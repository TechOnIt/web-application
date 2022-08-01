using iot.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace iot.Application.Common.Interfaces.Context;

public interface IIdentityContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    int SaveChanges();
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    public DbSet<User> Users { get; set; }
}