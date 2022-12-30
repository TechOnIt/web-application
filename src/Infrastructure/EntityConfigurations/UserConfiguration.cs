using TechOnIt.Domain.Entities.Identity.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechOnIt.Infrastructure.Common.Consts;

namespace TechOnIt.Infrastructure.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(a => a.Id); // primary key
        builder.Property(a => a.Id).ValueGeneratedNever();

        builder.OwnsOne(a => a.Password);
        builder.OwnsOne(a => a.FullName);
        builder.OwnsOne(a => a.ConcurrencyStamp);

        #region column types
        builder.Property(a => a.ConfirmedEmail).HasColumnType(DataTypes.boolean);
        builder.Property(a => a.IsBaned).HasColumnType(DataTypes.boolean);
        builder.Property(a => a.IsDeleted).HasColumnType(DataTypes.boolean);
        builder.Property(a => a.ConfirmedPhoneNumber).HasColumnType(DataTypes.boolean);

        builder.Property(a => a.Username).HasColumnType(DataTypes.nvarchar50);
        builder.Property(a => a.Email).HasColumnType(DataTypes.nvarchar50);
        builder.Property(a => a.PhoneNumber).HasColumnType(DataTypes.nvarchar50);

        builder.Property(a => a.MaxFailCount).HasColumnType(DataTypes.varchar50);
        #endregion

        #region indexing
        builder.HasIndex(b => b.PhoneNumber).IsUnique();
        #endregion
    }
}
