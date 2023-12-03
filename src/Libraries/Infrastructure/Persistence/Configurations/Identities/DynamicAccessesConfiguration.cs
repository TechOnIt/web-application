using TechOnIt.Domain.Entities.Identity;

namespace TechOnIt.Infrastructure.Persistence.Configurations.Identities;

public class DynamicAccessesConfiguration : IEntityTypeConfiguration<DynamicAccess>
{
    public void Configure(EntityTypeBuilder<DynamicAccess> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(a => a.Path).HasColumnType("nvarchar(500)");

        builder.HasOne(a => a.User)
            .WithMany(a => a.DynamicAccesses)
            .HasForeignKey(a => a.UserId);

        builder.HasIndex(a => new { a.Path, a.UserId })
            .IsUnique()
            .IsClustered(false)
            .HasDatabaseName("User_Path_UIX");
    }
}
