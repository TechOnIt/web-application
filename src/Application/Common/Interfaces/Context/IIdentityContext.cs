using Microsoft.EntityFrameworkCore;

namespace iot.Application.Common.Interfaces.Context;

public interface IIdentityContext
{
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    public DbSet<User> Users { get; set; }
    public DbSet<LoginHistory> LoginHistories { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
}