using TechOnIt.Domain.Entities.Product.SensorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechOnIt.Infrastructure.Common.Consts;

namespace TechOnIt.Infrastructure.EntityConfigurations.ProductEntityConfiguration;

public class PerformanceReportConfiguration : IEntityTypeConfiguration<PerformanceReport>
{
    public void Configure(EntityTypeBuilder<PerformanceReport> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Value).IsRequired();
        builder.Property(a => a.RecordDateTime).IsRequired();

        #region column types
        builder.Property(a => a.Id).HasColumnType(nameof(DataTypes.guid));
        builder.Property(a => a.Value).HasColumnType(nameof(DataTypes.numerics)).HasMaxLength(10);
        #endregion

        #region indexing
        builder.HasIndex(b => b.SensorId).IsUnique();
        #endregion
    }
}