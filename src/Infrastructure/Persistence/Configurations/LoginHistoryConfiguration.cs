using TechOnIt.Domain.Entities.Identity.UserAggregate;

namespace TechOnIt.Infrastructure.Persistence.Configurations;

public class LoginHistoryConfiguration : IEntityTypeConfiguration<LoginHistory>
{
    public void Configure(EntityTypeBuilder<LoginHistory> builder)
    {
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