using TechOnIt.Domain.Entities.Identity;

namespace TechOnIt.Infrastructure.Persistence.Configurations.Identities;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleEntity>
{
    public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
    {
        builder.ToTable("UserRole_Mapping", TableSchema.Identity);

        builder.HasKey(a => new { a.UserId, a.RoleId });
        // UserId
        builder.HasOne(a => a.User)
            .WithMany(a => a.UserRoles)
            .HasForeignKey(a => a.UserId);
        // RoleId
        builder.HasOne(a => a.Role)
            .WithMany(a => a.UserRoles)
            .HasForeignKey(a => a.RoleId);
    }
}
