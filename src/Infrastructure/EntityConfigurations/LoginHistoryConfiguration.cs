using TechOnIt.Domain.Entities.Identity.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TechOnIt.Infrastructure.EntityConfigurations;

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
