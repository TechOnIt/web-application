namespace TechOnIt.Infrastructure.Persistence.Configurations.ProductEntityConfiguration;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        // Id
        builder.HasKey(a => a.Id);
        builder.Property(b => b.Id)
            .ValueGeneratedNever();
        // Pin
        builder.Property(a => a.Pin)
            .ValueGeneratedNever()
            .IsRequired()
            .HasColumnType(DataTypes.numerics)
            .HasMaxLength(4);
        // Type
        builder.Property(a => a.Type)
            .IsRequired()
            .HasDefaultValue(DeviceType.Light)
            .HasColumnType(DataTypes.tinyint)
            .HasConversion(x => x.Value, x => Enumeration.FromValue<DeviceType>(x));
        // IsHigh
        builder.Property(a => a.IsHigh)
            .HasColumnType(DataTypes.boolean);
        // RowVersion
        builder.Property(a => a.RowVersion)
            .ValueGeneratedOnAddOrUpdate()
            .IsRowVersion()
            .IsConcurrencyToken(true);
        // PlaceId
        builder.HasOne(a => a.Place)
            .WithMany(a => a.Devices)
            .HasForeignKey(a => a.PlaceId);
    }
}