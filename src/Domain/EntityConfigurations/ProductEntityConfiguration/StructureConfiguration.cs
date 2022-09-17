using iot.Domain.Common;
using iot.Domain.Entities.Product.StructureAggregate;
using iot.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace iot.Domain.EntityConfigurations.ProductEntityConfiguration;

public class StructureConfiguration : IEntityTypeConfiguration<Structure>
{
    public void Configure(EntityTypeBuilder<Structure> builder)
    {
        builder.HasKey(a => a.Id);

        builder.OwnsOne(a => a.ApiKey); // define value object concurrency

        builder.Property(a => a.Type)
            .HasConversion(x => x.Value, x => Enumeration.FromValue<StuctureType>(x));
    }
}
