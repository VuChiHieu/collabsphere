using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectService.Models.Entities;

namespace ProjectService.Data.Configurations;

public class ProjectMilestoneConfiguration : IEntityTypeConfiguration<ProjectMilestone>
{
    public void Configure(EntityTypeBuilder<ProjectMilestone> builder)
    {
        builder.HasKey(pm => pm.MilestoneId);

        builder.HasIndex(pm => pm.ProjectId);
        builder.HasIndex(pm => pm.Order);
        builder.HasIndex(pm => new { pm.ProjectId, pm.MilestoneCode }).IsUnique();

        builder.Property(pm => pm.MilestoneCode).IsRequired().HasMaxLength(50);
        builder.Property(pm => pm.MilestoneName).IsRequired().HasMaxLength(300);
        builder.Property(pm => pm.Description).IsRequired().HasColumnType("text");
        builder.Property(pm => pm.Objectives).HasColumnType("text");
        builder.Property(pm => pm.Deliverables).HasColumnType("text");
        builder.Property(pm => pm.Weight).HasColumnType("decimal(5,2)").HasDefaultValue(0);
        builder.Property(pm => pm.IsRequired).HasDefaultValue(true);
    }
}