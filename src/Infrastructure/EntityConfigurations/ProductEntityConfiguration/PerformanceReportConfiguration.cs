using iot.Domain.Entities.Product.SensorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iot.Infrastructure.EntityConfigurations.ProductEntityConfiguration;

public class PerformanceReportConfiguration : IEntityTypeConfiguration<PerformanceReport>
{
    public void Configure(EntityTypeBuilder<PerformanceReport> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Value).IsRequired();
        builder.Property(a => a.RecordDateTime).IsRequired();
    }
}
