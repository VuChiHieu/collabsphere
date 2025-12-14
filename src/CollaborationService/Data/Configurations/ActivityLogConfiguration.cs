using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CollaborationService.Models.Entities;

namespace CollaborationService.Data.Configurations;

public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
{
    public void Configure(EntityTypeBuilder<ActivityLog> builder)
    {
        builder.HasKey(al => al.LogId);

        // Indexes
        builder.HasIndex(al => al.WorkspaceId);
        builder.HasIndex(al => al.EntityType);
        builder.HasIndex(al => al.EntityId);
        builder.HasIndex(al => al.ActionType);
        builder.HasIndex(al => al.PerformedBy);
        builder.HasIndex(al => al.PerformedAt);

        // Properties
        builder.Property(al => al.EntityType).IsRequired().HasMaxLength(50);
        builder.Property(al => al.ActionType).IsRequired().HasMaxLength(50);
        builder.Property(al => al.Changes).HasColumnType("text");
        builder.Property(al => al.PerformedAt).HasDefaultValueSql("NOW()");
    }
}