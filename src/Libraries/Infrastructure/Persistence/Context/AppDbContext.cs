using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using TechOnIt.Application.Common.Models;
using TechOnIt.Domain.Entities.Catalog;
using TechOnIt.Domain.Entities.Controllers;
using TechOnIt.Domain.Entities.General;
using TechOnIt.Domain.Entities.Identity;
using TechOnIt.Domain.Entities.Identity.UserAggregate;
using TechOnIt.Domain.Entities.Security;
using TechOnIt.Domain.Entities.SensorAggregate;

namespace TechOnIt.Infrastructure.Persistence.Context;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    #region DbSet

    #region Identity
    public DbSet<User> Users => Set<User>();
    public DbSet<LoginHistory> LoginHistories => Set<LoginHistory>();
    public DbSet<RoleEntity> Roles => Set<RoleEntity>();
    public DbSet<UserRoleEntity> UserRoles => Set<UserRoleEntity>();
    public DbSet<DynamicAccessEntity> DynamicAccesses => Set<DynamicAccessEntity>();
    #endregion

    #region Defaults
    public DbSet<Structure> Structures { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<RelayEntity> Relays { get; set; }
    public DbSet<SensorEntity> Sensors { get; set; }
    public DbSet<SensorReportEntity> SensorReports { get; set; }
    #endregion

    #region Metadata
    public DbSet<LogEntity> Logs { get; set; }
    #endregion

    #endregion

    #region Methods

    public override DbSet<TEntity> Set<TEntity>() where TEntity : class => base.Set<TEntity>();
    public override EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
    {
        return base.Entry(entity);
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
    public async Task<Result> SaveChangeAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            if (Convert.ToBoolean(await SaveChangesAsync(cancellationToken)))
                return Result.Ok();
            return Result.Fail();
        }
        catch (SqlException ex)
        {
            return Result.Fail(ex.Message, ex);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return Result.Fail(ex.Message, ex);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message, ex);
        }
    }
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await Database.BeginTransactionAsync();
    }

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

        optionsBuilder.UseSqlServer(configuration.GetConnectionString("Database"), options =>
        {
            options.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
        });
    }

    #endregion
}