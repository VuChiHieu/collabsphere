using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FileService.Models.Entities;

namespace FileService.Data.Configurations;

public class ResourceAccessConfiguration : IEntityTypeConfiguration<ResourceAccess>
{
    public void Configure(EntityTypeBuilder<ResourceAccess> builder)
    {
        builder.HasKey(ra => ra.AccessId);

        builder.HasIndex(ra => ra.ResourceId);
        builder.HasIndex(ra => ra.UserId);
        builder.HasIndex(ra => ra.AccessType);
        builder.HasIndex(ra => ra.AccessedAt);

        builder.Property(ra => ra.AccessType).IsRequired().HasMaxLength(50).HasDefaultValue("VIEW");
        builder.Property(ra => ra.IpAddress).HasMaxLength(50);
        builder.Property(ra => ra.UserAgent).HasMaxLength(500);
        builder.Property(ra => ra.AccessedAt).HasDefaultValueSql("NOW()");
    }
}