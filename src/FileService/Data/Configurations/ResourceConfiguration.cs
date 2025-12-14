using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FileService.Models.Entities;

namespace FileService.Data.Configurations;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.HasKey(r => r.ResourceId);

        builder.HasIndex(r => r.Scope);
        builder.HasIndex(r => r.ScopeId);
        builder.HasIndex(r => r.UploadedBy);
        builder.HasIndex(r => r.IsPublic);
        builder.HasIndex(r => new { r.Scope, r.ScopeId });

        builder.Property(r => r.ResourceName).IsRequired().HasMaxLength(300);
        builder.Property(r => r.FileUrl).IsRequired().HasMaxLength(500);
        builder.Property(r => r.FileType).HasMaxLength(100);
        builder.Property(r => r.Scope).HasMaxLength(50).HasDefaultValue("CLASS");
        builder.Property(r => r.Description).HasColumnType("text");
        builder.Property(r => r.ThumbnailUrl).HasMaxLength(500);
        builder.Property(r => r.Metadata).HasColumnType("text");
        builder.Property(r => r.DownloadCount).HasDefaultValue(0);
        builder.Property(r => r.ViewCount).HasDefaultValue(0);
        builder.Property(r => r.IsPublic).HasDefaultValue(false);

        builder.HasQueryFilter(r => !r.IsDeleted);

        builder.HasMany(r => r.ResourceAccesses)
                .WithOne(ra => ra.Resource)
                .HasForeignKey(ra => ra.ResourceId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.ResourceVersions)
                .WithOne(rv => rv.Resource)
                .HasForeignKey(rv => rv.ResourceId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}