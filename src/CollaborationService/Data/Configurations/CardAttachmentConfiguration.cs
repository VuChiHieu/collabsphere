using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CollaborationService.Models.Entities;

namespace CollaborationService.Data.Configurations;

public class CardAttachmentConfiguration : IEntityTypeConfiguration<CardAttachment>
{
    public void Configure(EntityTypeBuilder<CardAttachment> builder)
    {
        builder.HasKey(ca => ca.AttachmentId);

        // Indexes
        builder.HasIndex(ca => ca.CardId);
        builder.HasIndex(ca => ca.UploadedBy);

        // Properties
        builder.Property(ca => ca.FileName).IsRequired().HasMaxLength(300);
        builder.Property(ca => ca.FileUrl).IsRequired().HasMaxLength(500);
        builder.Property(ca => ca.FileType).HasMaxLength(100);
        builder.Property(ca => ca.ThumbnailUrl).HasMaxLength(500);

        // Soft delete
        builder.HasQueryFilter(ca => !ca.IsDeleted);
    }
}