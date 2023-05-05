using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechOnIt.Domain.Entities.Identity;

namespace TechOnIt.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        // Id
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .IsRequired()
            .ValueGeneratedNever();

        // Name
        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        // NormalizedName
        builder.Property(a => a.NormalizedName)
            .IsRequired(false)
            .HasMaxLength(100);
    }
}
