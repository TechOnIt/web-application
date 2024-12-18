﻿using TechOnIt.Domain.Entities.Identities;

namespace TechOnIt.Infrastructure.Persistence.Configurations.Identities;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.ToTable("Roles", TableSchema.Identity);
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