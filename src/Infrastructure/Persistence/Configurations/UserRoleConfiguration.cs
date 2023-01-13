﻿using TechOnIt.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechOnIt.Infrastructure.Common.Consts;

namespace TechOnIt.Infrastructure.Persistence.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(a => new { a.UserId, a.RoleId });

        builder.HasOne(a => a.User)
            .WithMany(a => a.UserRoles)
            .HasForeignKey(a => a.UserId);

        builder.HasOne(a => a.Role)
            .WithMany(a => a.UserRoles)
            .HasForeignKey(a => a.RoleId);
    }
}