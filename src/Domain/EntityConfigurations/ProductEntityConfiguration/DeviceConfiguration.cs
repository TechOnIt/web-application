using iot.Application.Common.Extentions;
using iot.Domain.Entities.Product;
using iot.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace iot.Domain.EntityConfigurations.ProductEntityConfiguration;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasOne("Place") // shadow property
            .WithMany("Devices")
            .HasForeignKey("PlaceId");

        builder.Property(a => a.Pin).IsRequired();

        builder.Property(a=>a.DeviceType)
            .HasConversion(x => x.Value, x => Enumeration.FromValue<DeviceType>(x));
    }
}
