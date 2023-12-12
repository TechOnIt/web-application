namespace TechOnIt.Infrastructure.Persistence.Configurations;

public class DeviceConfiguration : IEntityTypeConfiguration<RelayEntity>
{
    public void Configure(EntityTypeBuilder<RelayEntity> builder)
    {
        builder.ToTable("Devices", TableSchema.Default);

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
        // ConcurrencyStamp
        builder.Property(b => b.ConcurrencyStamp)
            .IsConcurrencyToken()
            .ValueGeneratedOnAddOrUpdate()
            .HasColumnType(DataTypes.rowVersion);
        // PlaceId
        builder.HasOne(b => b.Place)
            .WithMany(b => b.Devices)
            .HasForeignKey(b => b.PlaceId);
    }
}