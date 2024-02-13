using TechOnIt.Domain.Entities.Catalog;

namespace TechOnIt.Infrastructure.Persistence.Configurations;

public class StructureConfiguration : IEntityTypeConfiguration<Structure>
{
    public void Configure(EntityTypeBuilder<Structure> builder)
    {
        builder.ToTable("Structures", TableSchema.Default);

        // Id
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .ValueGeneratedNever();
        // Name
        builder.Property(a => a.Name)
            .IsRequired()
            .HasColumnType(DataTypes.nvarchar50);
        // Description
        builder.Property(a => a.Description)
            .IsRequired(false)
            .HasColumnType(DataTypes.nvarchar150);
        // Type
        builder.Property(s => s.Type)
            .HasConversion(t => t.Value, v => Enumeration.FromValue<StructureType>(v))
            .HasColumnType(DataTypes.tinyint);
        // ApiKey
        builder.OwnsOne(s => s.ApiKey, apiKey =>
        {
            apiKey.Property(c => c.Value)
            .HasColumnName("ApiKey")
            .HasColumnType(DataTypes.nvarchar100);
        });
        // Password
        builder.OwnsOne(s => s.Password, ph =>
        {
            ph.Property(pass => pass.Value)
            .HasColumnName("Password")
            .HasColumnType(DataTypes.nvarchar500);
        });
        // CreatedAt
        builder.Property(s => s.CreatedAt)
            .HasColumnType(DataTypes.datetime2);
        // ModifiedAt
        builder.Property(s => s.ModifiedAt)
            .IsRequired(false)
            .ValueGeneratedOnUpdate();
        // IsActive
        builder.Property(s => s.IsActive)
            .HasColumnType(DataTypes.boolean);
        // ConcurrencyStamp
        builder.Property(s => s.ConcurrencyStamp)
            .IsConcurrencyToken()
            .ValueGeneratedOnAddOrUpdate()
            .HasColumnType(DataTypes.rowVersion);
        // UserId
        builder.HasOne(s => s.User)
            .WithMany(s => s.Structures)
            .HasForeignKey(s => s.UserId);
        // Groups
        builder.HasMany(a => a.Groups)
            .WithOne(a => a.Structure)
            .HasForeignKey(a => a.StructureId);
    }
}