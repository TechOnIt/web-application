using iot.Domain.Entities.Identity;
using iot.Domain.Entities.Product;
using iot.Domain.Entities.Product.SensorAggregate;
using iot.Domain.Entities.Product.StructureAggregate;
using iot.Domain.Entities.Secyrity;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace iot.Infrastructure.Persistence.Context.Identity;

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
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        optionsBuilder.UseSqlServer(configuration.GetConnectionString("IdentityDevelopment"));
        //optionsBuilder.UseSqlServer(configuration.GetConnectionString("DeskDevelopment"));
        //optionsBuilder.UseSqlServer(configuration.GetConnectionString("CoreDevelopment"));
    }


    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<LoginHistory> LoginHistories { get; set; }

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