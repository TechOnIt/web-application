using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using TechOnIt.Domain.Entities.Catalogs;
using TechOnIt.Domain.Entities.Controllers;
using TechOnIt.Domain.Entities.Generals;
using TechOnIt.Domain.Entities.Identities;
using TechOnIt.Domain.Entities.Identities.UserAggregate;
using TechOnIt.Domain.Entities.Securities;
using TechOnIt.Domain.Entities.Sensors;

namespace TechOnIt.Application.Common.Interfaces
{
    public interface IAppDbContext
    {

        #region Identity

        DbSet<UserEntity> Users { get; }
        DbSet<LoginHistory> LoginHistories { get; }
        DbSet<RoleEntity> Roles { get; }
        DbSet<UserRoleEntity> UserRoles { get; }
        DbSet<DynamicAccessEntity> DynamicAccesses { get; }

        #endregion

        #region Defaults

        DbSet<Structure> Structures { get; }
        DbSet<Group> Groups { get; }
        DbSet<RelayEntity> Relays { get; }
        DbSet<SensorEntity> Sensors { get; }
        DbSet<SensorReportEntity> SensorReports { get; }

        #endregion

        #region Metadata
        DbSet<LogEntity> Logs { get; }
        #endregion

        ChangeTracker ChangeTracker { get; }
        DatabaseFacade Database { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<Result> SaveChangeAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
