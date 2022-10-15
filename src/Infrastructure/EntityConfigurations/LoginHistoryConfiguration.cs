using iot.Domain.Entities.Identity.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iot.Infrastructure.EntityConfigurations;

public class LoginHistoryConfiguration : IEntityTypeConfiguration<LoginHistory>
{
    public void Configure(EntityTypeBuilder<LoginHistory> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasOne(a => a.User)
            .WithMany(a => a.LoginHistories)
            .HasForeignKey(a => a.UserId);

        builder.OwnsOne(a => a.Ip);
    }
}
