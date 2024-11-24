using TechOnIt.Domain.Entities.Sensors;

namespace TechOnIt.Infrastructure.Persistence.Configurations;

public class SensorConfiguration : IEntityTypeConfiguration<SensorEntity>
{
    public void Configure(EntityTypeBuilder<SensorEntity> builder)
    {
        builder.ToTable("Sensors", TableSchema.Default);

        // Id
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .IsRequired()
            .ValueGeneratedNever();
        // Pin
        builder.Property(b => b.Pin)
            .IsRequired();
        // Type
        builder.Property(a => a.Type)
            .IsRequired()
            .HasColumnType(DataTypes.tinyint)
            .HasDefaultValue(SensorType.Thermometer)
            .HasConversion(x => x.Value, x => Enumeration.FromValue<SensorType>(x));
        // CreatedAt
        builder.Property(b => b.CreatedAt)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasColumnType(DataTypes.datetime2);
        // ModifiedAt
        builder.Property(b => b.CreatedAt)
            .IsRequired()
            .ValueGeneratedOnUpdate()
            .HasColumnType(DataTypes.datetime2);
        // GroupId
        builder.Property(b => b.GroupId)
            .IsRequired();
        // Group
        builder.HasOne(s => s.Group)
            .WithMany(s => s.Sensors)
            .HasForeignKey(s => s.GroupId);
        // Reports
        builder.HasMany(a => a.Reports)
            .WithOne(a => a.Sensor)
            .HasForeignKey(a => a.SensorId);
    }
}