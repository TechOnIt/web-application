using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechOnIt.Domain.Entities;
using TechOnIt.Domain.Enums;
using TechOnIt.Infrastructure.Common.Consts;

namespace TechOnIt.Infrastructure.Persistence.Configurations;

public class LogRecordConfiguration : IEntityTypeConfiguration<LogRecord>
{
    public void Configure(EntityTypeBuilder<LogRecord> builder)
    {
        // Id
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id)
            .ValueGeneratedOnAdd();

        // ShortMessage
        builder.HasIndex(l => l.ShortMessage);
        builder.Property(l => l.ShortMessage)
            .HasColumnType(DataTypes.nvarchar500)
            .IsRequired();

        // LevelId
        builder.Property(l => l.LevelId)
            .HasColumnType(DataTypes.tinyint)
            .HasDefaultValue(LogLevelType.Information);

        // PresentationId
        builder.Property(l => l.PresentationId)
            .HasColumnType(DataTypes.tinyint)
            .HasDefaultValue(PresentationAssembly.None);

        // FullMessage
        builder.Property(l => l.FullMessage)
            .HasColumnType(DataTypes.nvarchar4000);

        // IpAddreess
        builder.Property(l => l.IpAddress)
            .HasColumnType(DataTypes.varchar15);

        // Url
        builder.Property(l => l.Url)
            .HasColumnType(DataTypes.nvarchar100);

        // ReferrerUrl
        builder.Property(l => l.ReferrerUrl)
            .HasColumnType(DataTypes.nvarchar100);
        
        // UserId
        builder.Property(l => l.UserId)
            .HasColumnType(DataTypes.guid);

        // User - Relation
        builder.HasOne(l => l.User)
            .WithMany(l => l.LogHistories)
            .HasForeignKey(l => l.UserId);

        // CreatedAt
        builder.Property(l => l.CreatedAt)
            .HasColumnType(DataTypes.datetime2);
    }
}