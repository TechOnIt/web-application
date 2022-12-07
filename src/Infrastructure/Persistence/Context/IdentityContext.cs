using TechOnIt.Domain.Entities.Identity;
using TechOnIt.Domain.Entities.Identity.UserAggregate;
using TechOnIt.Domain.Entities.Product;
using TechOnIt.Domain.Entities.Product.SensorAggregate;
using TechOnIt.Domain.Entities.Product.StructureAggregate;
using TechOnIt.Domain.Entities.Secyrity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace TechOnIt.Infrastructure.Persistence.Context;

public class IdentityContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //In this case, the predicate will scan the types and for each type, will check if it implements the IEntityTypeConfiguration<T> interface and if T inherits BaseEntity.
        //https://stackoverflow.com/questions/61430833/applyconfigurationsfromassembly-with-filter-entityframework-core
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        optionsBuilder.UseSqlServer(configuration.GetConnectionString("IdentityDevelopment"));
        //optionsBuilder.UseSqlServer(configuration.GetConnectionString("DeskDevelopment"));
        //optionsBuilder.UseSqlServer(configuration.GetConnectionString("CoreDevelopment"));
    }

    #region Identity
    public DbSet<User> Users { get; set; }
    public DbSet<LoginHistory> LoginHistories { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    #endregion

    #region products
    public DbSet<Structure> Structures { get; set; }
    public DbSet<Place> Places { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<Sensor> Sensors { get; set; }
    public DbSet<PerformanceReport> PerformanceReports { get; set; }

    #endregion

    #region keysStor for data protection
    public DbSet<AesKey> AesKeys { get; set; }
    #endregion
}