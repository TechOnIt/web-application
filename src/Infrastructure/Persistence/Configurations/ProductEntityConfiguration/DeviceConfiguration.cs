using TechOnIt.Domain.Common;
using TechOnIt.Domain.Entities.Product;
using TechOnIt.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using System.Xml;
using TechOnIt.Infrastructure.Common.Consts;

namespace TechOnIt.Infrastructure.Persistence.Configurations.ProductEntityConfiguration;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasOne(a => a.Place) // shadow property
            .WithMany(a => a.Devices)
            .HasForeignKey(a => a.PlaceId);

        builder.Property(a => a.Pin).IsRequired();

        builder.Property(a => a.DeviceType)
            .HasConversion(x => x.Value, x => Enumeration.FromValue<DeviceType>(x));

        //Concurrency Settings
        builder.Property(a => a.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken(true);

        #region column types
        builder.Property(a => a.Pin).HasColumnType(DataTypes.numerics).HasMaxLength(10);
        builder.Property(a => a.IsHigh).HasColumnType(DataTypes.boolean);
        #endregion
    }
}