using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FileService.Models.Entities;

namespace FileService.Data.Configurations;

public class SharedDocumentConfiguration : IEntityTypeConfiguration<SharedDocument>
{
    public void Configure(EntityTypeBuilder<SharedDocument> builder)
    {
        builder.HasKey(sd => sd.DocumentId);

        builder.HasIndex(sd => sd.TeamId);
        builder.HasIndex(sd => sd.IsLocked);
        builder.HasIndex(sd => sd.LockedBy);

        builder.Property(sd => sd.DocumentName).IsRequired().HasMaxLength(300);
        builder.Property(sd => sd.DocumentType).HasMaxLength(50).HasDefaultValue("TEXT");
        builder.Property(sd => sd.Content).IsRequired().HasColumnType("text");
        builder.Property(sd => sd.IsLocked).HasDefaultValue(false);
        builder.Property(sd => sd.Version).HasDefaultValue(1);

        builder.HasQueryFilter(sd => !sd.IsDeleted);

        builder.HasMany(sd => sd.DocumentCollaborators)
                .WithOne(dc => dc.SharedDocument)
                .HasForeignKey(dc => dc.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}