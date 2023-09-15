using TechOnIt.Domain.Entities.Security;

namespace TechOnIt.Infrastructure.Persistence.Configurations.Security;

public class PermissionRecordConfiguration : IEntityTypeConfiguration<PermissionRecord>
{
    public void Configure(EntityTypeBuilder<PermissionRecord> builder)
    {
        builder.ToTable("Permissions", TableSchema.Identity);

        // Id
        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        // Name
        builder.Property(p => p.Name)
            .HasMaxLength(100);

        // System name
        builder.Property(p => p.SystemName)
            .HasMaxLength(100);
    }
}
