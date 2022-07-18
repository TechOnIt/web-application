using iot.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace iot.Application.Common.Services;

internal interface IIdentityDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<UserRole> UsersRoles { get; set; }
    DbSet<LoginHistory> LoginHistories { get; set; }
}