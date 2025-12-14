using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FileService.Models.Entities;

namespace FileService.Data.Configurations;

public class ResourceVersionConfiguration : IEntityTypeConfiguration<ResourceVersion>
{
    public void Configure(EntityTypeBuilder<ResourceVersion> builder)
    {
        builder.HasKey(rv => rv.VersionId);

        builder.HasIndex(rv => rv.ResourceId);
        builder.HasIndex(rv => rv.VersionNumber);
        builder.HasIndex(rv => new { rv.ResourceId, rv.VersionNumber }).IsUnique();
        builder.HasIndex(rv => rv.IsCurrent);

        builder.Property(rv => rv.FileUrl).IsRequired().HasMaxLength(500);
        builder.Property(rv => rv.ChangeDescription).HasColumnType("text");
        builder.Property(rv => rv.IsCurrent).HasDefaultValue(false);
    }
}