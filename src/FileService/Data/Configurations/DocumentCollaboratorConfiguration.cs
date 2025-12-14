using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FileService.Models.Entities;

namespace FileService.Data.Configurations;

public class DocumentCollaboratorConfiguration : IEntityTypeConfiguration<DocumentCollaborator>
{
    public void Configure(EntityTypeBuilder<DocumentCollaborator> builder)
    {
        builder.HasKey(dc => dc.CollaboratorId);

        builder.HasIndex(dc => dc.DocumentId);
        builder.HasIndex(dc => dc.UserId);
        builder.HasIndex(dc => new { dc.DocumentId, dc.UserId }).IsUnique();

        builder.Property(dc => dc.Permission).HasMaxLength(50).HasDefaultValue("VIEW");
        builder.Property(dc => dc.IsActive).HasDefaultValue(true);
    }
}