using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechOnIt.Domain.Common;
using TechOnIt.Domain.Entities;
using TechOnIt.Domain.Enums;

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

        builder.Property(a => a.Type)
            .HasConversion(x => x.Value, x => Enumeration.FromValue<DeviceType>(x));

        //Concurrency Settings
        builder.Property(a => a.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken(true);

        #region column types
        builder.Property(a => a.Pin).HasColumnType(DataTypes.numerics).HasMaxLength(10);
        builder.Property(a => a.IsHigh).HasColumnType(DataTypes.boolean);
        builder.Property(a => a.Type).HasColumnType(DataTypes.tinyint);
        #endregion
    }
}