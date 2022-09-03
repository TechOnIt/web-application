using iot.Application.Common.Extentions;
using iot.Domain.Entities.Product;
using iot.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace iot.Domain.EntityConfigurations.ProductEntityConfiguration;

public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
{
    public void Configure(EntityTypeBuilder<Sensor> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.SensorType).IsRequired();

        builder
          .Property(t => t.SensorType)
          .HasConversion(x => x.Value, x => Enumeration.FromValue<SensorType>(x));
        // https://stackoverflow.com/questions/37513374/using-enumeration-classes-in-ef
    }
}
