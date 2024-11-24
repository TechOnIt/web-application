using TechOnIt.Domain.Entities.Catalogs;

namespace TechOnIt.Infrastructure.Persistence.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<GroupEntity>
{
    public void Configure(EntityTypeBuilder<GroupEntity> builder)
    {
        builder.ToTable("Groups", TableSchema.Default);

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
        // StructureId
        builder.HasOne(b => b.Structure)
            .WithMany(b => b.Groups)
            .HasForeignKey(b => b.StructureId);
        // Relays
        builder.HasMany(a => a.Relays)
            .WithOne(a => a.Group)
            .HasForeignKey(a => a.GroupId);
    }
}