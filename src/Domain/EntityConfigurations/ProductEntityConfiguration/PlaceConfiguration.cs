using iot.Domain.Entities.Product.StructureAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace iot.Domain.EntityConfigurations.ProductEntityConfiguration;

public class PlaceConfiguration : IEntityTypeConfiguration<Place>
{
    public void Configure(EntityTypeBuilder<Place> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).IsRequired();
        builder.Property(a => a.CreateDate).IsRequired();

        builder.HasOne(a => a.Structure)
            .WithMany(a => a.Places)
            .HasForeignKey(a => a.StuctureId);
    }
}
