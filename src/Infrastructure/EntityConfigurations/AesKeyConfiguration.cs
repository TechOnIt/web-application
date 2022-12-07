using TechOnIt.Domain.Entities.Secyrity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TechOnIt.Infrastructure.EntityConfigurations;

public class AesKeyConfiguration : IEntityTypeConfiguration<AesKey>
{
    public void Configure(EntityTypeBuilder<AesKey> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasData(
            new AesKey
            {
                Id = Guid.NewGuid(),
                Title = "DeviceKey",
                Key = GetKey()
            },
            new AesKey
            {
                Id = Guid.NewGuid(),
                Title = "UserKey",
                Key = GetKey()
            },
            new AesKey
            {
                Id = Guid.NewGuid(),
                Title = "SesnsorKey",
                Key = GetKey()
            },
            new AesKey
            {
                Id = Guid.NewGuid(),
                Title = "ReportKey",
                Key = GetKey()
            }
            );
    }

    private string GetKey()
    {
        using (var aes = System.Security.Cryptography.Aes.Create())
        {
            return Convert.ToBase64String(aes.Key);
        }
    }
}
