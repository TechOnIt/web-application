using iot.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace iot.Domain.EntityConfigurations.ProductEntityConfiguration;

public class PerformanceReportConfiguration : IEntityTypeConfiguration<PerformanceReport>
{
    public void Configure(EntityTypeBuilder<PerformanceReport> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Value).IsRequired();
        builder.Property(a => a.RecordDateTime).IsRequired();
    }
}
