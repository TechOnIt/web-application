using TechOnIt.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechOnIt.Infrastructure.Common.Consts;

namespace TechOnIt.Infrastructure.EntityConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedNever();

        #region column types
        builder.Property(a => a.Id).HasColumnType(nameof(DataTypes.guid));
        builder.Property(a => a.Name).HasColumnType(nameof(DataTypes.varchar50));
        builder.Property(a => a.NormalizedName).HasColumnType(nameof(DataTypes.varchar50));
        #endregion
    }
}
