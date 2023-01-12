using TechOnIt.Domain.Common;
using TechOnIt.Domain.Entities.Product.StructureAggregate;
using TechOnIt.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechOnIt.Infrastructure.Common.Consts;

namespace TechOnIt.Infrastructure.Persistence.Configurations.ProductEntityConfiguration;

public class StructureConfiguration : IEntityTypeConfiguration<Structure>
{
    public void Configure(EntityTypeBuilder<Structure> builder)
    {
        builder.HasKey(a => a.Id);

        builder.OwnsOne(a => a.ApiKey); // define value object concurrency

        builder.OwnsOne(a => a.PasswordHash);

        builder.Property(a => a.Type)
            .HasConversion(x => x.Value, x => Enumeration.FromValue<StuctureType>(x));

        #region column types
        builder.Property(a => a.Description).HasColumnType(DataTypes.nvarchar150);
        builder.Property(a => a.Name).HasColumnType(DataTypes.nvarchar50);
        // TODO:
        // FIX THIS!
        //builder.Property(a => a.ApiKey).HasColumnType(DataTypes.nvarchar50);
        //builder.Property(a => a.PasswordHash).HasColumnType(DataTypes.nvarchar150);

        builder.Property(a => a.IsActive).HasColumnType(DataTypes.boolean);
        #endregion
    }
}