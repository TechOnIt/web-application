using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechOnIt.Domain.Entities.Identity.UserAggregate;

namespace TechOnIt.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Id
        builder.HasKey(u => u.Id);
        builder.Property(a => a.Id)
            .ValueGeneratedNever();
        // Username - index
        builder.HasIndex(u => u.Username)
            .IsUnique();
        builder.Property(u => u.Username)
            .IsRequired()
            .HasColumnType(DataTypes.nvarchar50);
        // Password
        builder.OwnsOne(u => u.Password, ph =>
        {
            ph.Property(u => u.Value)
            .HasColumnName("Password")
            .HasColumnType(DataTypes.nvarchar500);
        });

        // Email
        builder.Property(u => u.Email)
            .HasColumnType(DataTypes.nvarchar50);

        // ConfirmedEmail
        builder.Property(u => u.ConfirmedEmail)
            .HasColumnType(DataTypes.boolean);

        // PhoneNumber
        builder.Property(u => u.PhoneNumber)
            .HasColumnType(DataTypes.nvarchar50);

        // ConfirmedPhoneNumber
        builder.Property(u => u.ConfirmedPhoneNumber)
            .HasColumnType(DataTypes.boolean);

        // FullName
        builder.OwnsOne(u => u.FullName, b =>
            {
                b.Property(u => u.Name)
                .HasColumnName("Name")
                .HasColumnType(DataTypes.nvarchar50);
                b.Property(u => u.Surname)
                .HasColumnName("Surname")
                .HasColumnType(DataTypes.nvarchar50);
            });

        // RegisteredDateTime
        builder.Property(u => u.RegisteredAt)
            .HasColumnType(DataTypes.datetime2);

        // ConcurrencyStamp
        builder.Property(b => b.ConcurrencyStamp)
            .ValueGeneratedOnAddOrUpdate()
            .IsRowVersion()
            .IsConcurrencyToken(true)
            .HasColumnType(DataTypes.nvarchar500);
        // IsBaned
        builder.Property(u => u.IsBaned)
            .HasColumnType(DataTypes.boolean);
        // IsDeleted
        builder.Property(u => u.IsDeleted)
            .HasColumnType(DataTypes.boolean);

        // MaxFailCount
        builder.Property(u => u.MaxFailCount)
            .HasColumnType(DataTypes.varchar50);

        // LockOutDateTime
        builder.Property(u => u.LockOutDateTime)
            .IsRequired(false)
            .HasColumnType(DataTypes.datetime2);
    }
}
