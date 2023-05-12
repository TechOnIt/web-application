using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechOnIt.Domain.Common;
using TechOnIt.Domain.Entities.StructureAggregate;
using TechOnIt.Domain.Enums;

namespace TechOnIt.Infrastructure.Persistence.Configurations.ProductEntityConfiguration;

public class StructureConfiguration : IEntityTypeConfiguration<Structure>
{
    public void Configure(EntityTypeBuilder<Structure> builder)
    {
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
            .ValueGeneratedOnAddOrUpdate()
            .IsRowVersion()
            .IsConcurrencyToken()
            .HasColumnType(DataTypes.nvarchar500);
        // UserId
        builder.HasOne(s => s.User)
            .WithMany(s => s.Structures)
            .HasForeignKey(s => s.UserId);
        // Places
        builder.HasMany(a => a.Places)
            .WithOne(a => a.Structure)
            .HasForeignKey(a => a.StructureId);
    }
}