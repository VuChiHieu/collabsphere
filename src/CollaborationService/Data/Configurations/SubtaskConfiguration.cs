using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CollaborationService.Models.Entities;

namespace CollaborationService.Data.Configurations;

public class SubtaskConfiguration : IEntityTypeConfiguration<Subtask>
{
    public void Configure(EntityTypeBuilder<Subtask> builder)
    {
        builder.HasKey(s => s.SubtaskId);

        // Indexes
        builder.HasIndex(s => s.TaskId);
        builder.HasIndex(s => s.Order);
        builder.HasIndex(s => s.IsCompleted);

        // Properties
        builder.Property(s => s.SubtaskTitle).IsRequired().HasMaxLength(300);
        builder.Property(s => s.Status).HasMaxLength(50).HasDefaultValue("TODO");
        builder.Property(s => s.Order).HasDefaultValue(0);
        builder.Property(s => s.IsCompleted).HasDefaultValue(false);
    }
}