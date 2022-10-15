using iot.Domain.Common;
using iot.Domain.Entities.Product;
using iot.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using System.Xml;

namespace iot.Infrastructure.EntityConfigurations.ProductEntityConfiguration;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasOne(a=>a.Place) // shadow property
            .WithMany(a=>a.Devices)
            .HasForeignKey(a=>a.PlaceId);

        builder.Property(a => a.Pin).IsRequired();

        builder.Property(a=>a.DeviceType)
            .HasConversion(x => x.Value, x => Enumeration.FromValue<DeviceType>(x));

        //Concurrency Settings
        builder.Property(a => a.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken(true);
    }
}