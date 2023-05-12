using TechOnIt.Domain.Entities.SensorAggregate;

namespace TechOnIt.Infrastructure.Persistence.Configurations.ProductEntityConfiguration;

public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
{
    public void Configure(EntityTypeBuilder<Sensor> builder)
    {
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
            .HasConversion(x => x.Value, x => Enumeration.FromValue<SensorType>(x));
        // CreatedAt
        builder.Property(b => b.CreatedAt)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasColumnType(DataTypes.datetime2);
        // ModifiedAt
        builder.Property(b => b.CreatedAt)
            .IsRequired(false)
            .ValueGeneratedOnUpdate()
            .HasColumnType(DataTypes.datetime2);
        // PlaceId
        builder.Property(b => b.Place)
            .IsRequired();
        // Place
        builder.HasOne(s => s.Place)
            .WithMany(s => s.Sensors)
            .HasForeignKey(s => s.PlaceId);
        // Reports
        builder.HasMany(a => a.Reports)
            .WithOne(a => a.Sensor)
            .HasForeignKey(a => a.SensorId);
    }
}
