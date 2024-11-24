using TechOnIt.Domain.Entities.Sensors;

namespace TechOnIt.Infrastructure.Persistence.Configurations;

public class SensorReportConfiguration : IEntityTypeConfiguration<SensorReportEntity>
{
    public void Configure(EntityTypeBuilder<SensorReportEntity> builder)
    {
        builder.ToTable("SensorReports", TableSchema.Default);

        // Id
        builder.HasKey(a => a.Id);
        builder.Property(b => b.Id)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnType(DataTypes.guid);
        // Value
        builder.Property(a => a.Value)
            .IsRequired();
        // CreatedAt
        builder.Property(a => a.CreatedAt)
            .IsRequired()
            .ValueGeneratedOnAdd();
        // SensorId
        builder.HasIndex(b => b.SensorId);
        // Sensor relation
        builder.HasOne(b => b.Sensor)
            .WithMany(b => b.Reports)
            .HasForeignKey(b => b.SensorId);
    }
}