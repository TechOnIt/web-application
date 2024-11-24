using TechOnIt.Domain.Entities.Identities.UserAggregate;

namespace TechOnIt.Infrastructure.Persistence.Configurations.Identities;

public class LoginHistoryConfiguration : IEntityTypeConfiguration<LoginActivityEntity>
{
    public void Configure(EntityTypeBuilder<LoginActivityEntity> builder)
    {
        builder.ToTable("LoginHistories", TableSchema.Identity);

        // Id
        builder.HasKey(a => a.Id);
        // Ip
        builder.OwnsOne(b => b.Ip, ip =>
        {
            ip.Property(ipv4 => ipv4.Value)
            .HasColumnName("Ip")
            .HasColumnType(DataTypes.nvarchar15);
        });
        // CreatedAt
        builder.Property(b => b.CreatedAt)
            .IsRequired();
        // UserId
        builder.Property(b => b.UserId)
            .IsRequired();
        // User
        builder.HasOne(a => a.User)
            .WithMany(a => a.LoginHistories)
            .HasForeignKey(a => a.UserId);
    }
}