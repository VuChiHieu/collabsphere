using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EntityTask = CollaborationService.Models.Entities.Task; 

namespace CollaborationService.Data.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<EntityTask>
{
    public void Configure(EntityTypeBuilder<EntityTask> builder)
    {
        builder.HasKey(t => t.TaskId);

        // Indexes
        builder.HasIndex(t => t.CardId);
        builder.HasIndex(t => t.Status);
        builder.HasIndex(t => t.DueDate);
        builder.HasIndex(t => t.IsCompleted);

        // Properties
        builder.Property(t => t.TaskTitle).IsRequired().HasMaxLength(300);
        builder.Property(t => t.Description).HasColumnType("text");
        builder.Property(t => t.Status).HasMaxLength(50).HasDefaultValue("TODO");
        builder.Property(t => t.Order).HasDefaultValue(0);
        builder.Property(t => t.IsCompleted).HasDefaultValue(false);

        // Relationships
        builder.HasMany(t => t.Subtasks)
               .WithOne(s => s.Task)
               .HasForeignKey(s => s.TaskId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.TaskAssignments)
               .WithOne(ta => ta.Task)
               .HasForeignKey(ta => ta.TaskId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}