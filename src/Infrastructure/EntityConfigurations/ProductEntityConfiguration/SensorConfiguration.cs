using TechOnIt.Domain.Common;
using TechOnIt.Domain.Entities.Product.SensorAggregate;
using TechOnIt.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechOnIt.Infrastructure.Common.Consts;

namespace TechOnIt.Infrastructure.EntityConfigurations.ProductEntityConfiguration;

public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
{
    public void Configure(EntityTypeBuilder<Sensor> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.SensorType).IsRequired();

        builder.HasMany(a => a.Reports)
            .WithOne(a => a.Sensor)
            .HasForeignKey(a => a.SensorId);

        builder
          .Property(t => t.SensorType)
          .HasConversion(x => x.Value, x => Enumeration.FromValue<SensorType>(x));
        // https://stackoverflow.com/questions/37513374/using-enumeration-classes-in-ef
    }
}
