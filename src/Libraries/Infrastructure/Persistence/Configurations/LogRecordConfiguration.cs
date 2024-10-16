using TechOnIt.Domain.Entities.General;

namespace TechOnIt.Infrastructure.Persistence.Configurations;

public class LogRecordConfiguration : IEntityTypeConfiguration<LogEntity>
{
    public void Configure(EntityTypeBuilder<LogEntity> builder)
    {
        builder.ToTable("Logs", TableSchema.Metadata);

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