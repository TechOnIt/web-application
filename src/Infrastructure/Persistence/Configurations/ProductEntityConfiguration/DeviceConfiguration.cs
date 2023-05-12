namespace TechOnIt.Infrastructure.Persistence.Configurations.ProductEntityConfiguration;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        // Id
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .ValueGeneratedNever();
        // Pin
        builder.Property(b => b.Pin)
            .ValueGeneratedNever()
            .IsRequired()
            .HasColumnType(DataTypes.numerics)
            .HasMaxLength(4);
        // Type
        builder.Property(b => b.Type)
            .IsRequired()
            .HasDefaultValue(DeviceType.Light)
            .HasColumnType(DataTypes.tinyint)
            .HasConversion(x => x.Value, x => Enumeration.FromValue<DeviceType>(x));
        // IsHigh
        builder.Property(b => b.IsHigh)
            .HasColumnType(DataTypes.boolean);
        // RowVersion
        builder.Property(b => b.RowVersion)
            .ValueGeneratedOnAddOrUpdate()
            .IsRowVersion()
            .IsConcurrencyToken(true)
            .HasColumnType(DataTypes.nvarchar500);
        // PlaceId
        builder.HasOne(b => b.Place)
            .WithMany(b => b.Devices)
            .HasForeignKey(b => b.PlaceId);
    }
}