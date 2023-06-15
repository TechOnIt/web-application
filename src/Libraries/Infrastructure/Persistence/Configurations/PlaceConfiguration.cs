using TechOnIt.Domain.Entities.StructureAggregate;

namespace TechOnIt.Infrastructure.Persistence.Configurations;

public class PlaceConfiguration : IEntityTypeConfiguration<Place>
{
    public void Configure(EntityTypeBuilder<Place> builder)
    {
        builder.ToTable("Places", TableSchema.Default);

        // Id
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .IsRequired()
            .ValueGeneratedNever();
        // Name
        builder.Property(b => b.Name)
            .IsRequired()
            .HasColumnType(DataTypes.nvarchar50);
        // Description
        builder.Property(b => b.Description)
            .IsRequired(false)
            .HasColumnType(DataTypes.nvarchar150);
        // CreatedAt
        builder.Property(b => b.CreatedAt)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasColumnType(DataTypes.datetime2);
        // ModifiedAt
        builder.Property(b => b.ModifiedAt)
            .ValueGeneratedOnUpdate()
            .IsRequired(false)
            .HasColumnType(DataTypes.datetime2);
        // StructureId
        builder.HasOne(b => b.Structure)
            .WithMany(b => b.Places)
            .HasForeignKey(b => b.StructureId);
        // Devices
        builder.HasMany(a => a.Devices)
            .WithOne(a => a.Place)
            .HasForeignKey(a => a.PlaceId);
    }
}