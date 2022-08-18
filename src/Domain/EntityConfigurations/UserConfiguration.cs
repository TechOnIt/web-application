using iot.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot.Domain.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(a => a.Id); // primary key
            builder.Property(a => a.Id).ValueGeneratedNever();

            builder.OwnsOne(a => a.Password);
            builder.OwnsOne(a=> a.FullName);
            builder.OwnsOne(a=> a.ConcurrencyStamp);
        }
    }
}
