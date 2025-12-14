using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CollaborationService.Models.Entities;

namespace CollaborationService.Data.Configurations;

public class WorkspaceConfiguration : IEntityTypeConfiguration<Workspace>
{
    public void Configure(EntityTypeBuilder<Workspace> builder)
    {
        builder.HasKey(w => w.WorkspaceId);

        // Indexes
        builder.HasIndex(w => w.TeamId).IsUnique(); // One workspace per team
        builder.HasIndex(w => w.IsActive);

        // Properties
        builder.Property(w => w.WorkspaceName).IsRequired().HasMaxLength(200);
        builder.Property(w => w.Description).HasColumnType("text");
        builder.Property(w => w.ViewMode).HasMaxLength(50).HasDefaultValue("KANBAN");
        builder.Property(w => w.Settings).HasColumnType("text");
        builder.Property(w => w.IsActive).HasDefaultValue(true);

        // Relationships
        builder.HasMany(w => w.Sprints)
               .WithOne(s => s.Workspace)
               .HasForeignKey(s => s.WorkspaceId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(w => w.Boards)
               .WithOne(b => b.Workspace)
               .HasForeignKey(b => b.WorkspaceId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(w => w.ActivityLogs)
               .WithOne(al => al.Workspace)
               .HasForeignKey(al => al.WorkspaceId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}