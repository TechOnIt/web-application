using iot.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace iot.Infrastructure.Persistence.Context;

public class IdentityContext : DbContext, IIdentityContext
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options) { }

#nullable disable
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<LoginHistory> LoginHistories { get; set; }
#nullable enable
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}