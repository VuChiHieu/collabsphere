using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CollaborationService.Models.Entities;

namespace CollaborationService.Data.Configurations;

public class TaskAssignmentConfiguration : IEntityTypeConfiguration<TaskAssignment>
{
    public void Configure(EntityTypeBuilder<TaskAssignment> builder)
    {
        builder.HasKey(ta => ta.AssignmentId);

        // Indexes
        builder.HasIndex(ta => ta.TaskId);
        builder.HasIndex(ta => ta.AssigneeId);
        builder.HasIndex(ta => new { ta.TaskId, ta.AssigneeId }).IsUnique();

        // Properties
        builder.Property(ta => ta.AssignedAt).HasDefaultValueSql("NOW()");
        builder.Property(ta => ta.IsActive).HasDefaultValue(true);
    }
}