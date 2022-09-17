using iot.Domain.Entities.Identity;
using iot.Domain.Entities.Product.StructureAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace iot.Infrastructure.Persistence.Context.Identity;

public interface IIdentityContext : IContext
{
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    public DatabaseFacade Database { get; }

    public DbSet<User> Users { get; set; }
    public DbSet<LoginHistory> LoginHistories { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    public DbSet<Structure> Structures { get; set; }
    public DbSet<Place> Places { get; set; }
}