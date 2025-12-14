using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamService.Models.Entities;

namespace TeamService.Data.Configurations;

public class TeamMilestoneConfiguration : IEntityTypeConfiguration<TeamMilestone>
{
    public void Configure(EntityTypeBuilder<TeamMilestone> builder)
    {
        builder.HasKey(tm => tm.TeamMilestoneId);

        // Indexes
        builder.HasIndex(tm => tm.TeamId);
        builder.HasIndex(tm => tm.ProjectMilestoneId);
        builder.HasIndex(tm => new { tm.TeamId, tm.ProjectMilestoneId }).IsUnique();
        builder.HasIndex(tm => tm.Status);

        // Properties
        builder.Property(tm => tm.Status).HasMaxLength(50).HasDefaultValue("NOT_STARTED");
        builder.Property(tm => tm.Progress).HasColumnType("decimal(5,2)").HasDefaultValue(0);
        builder.Property(tm => tm.Score).HasColumnType("decimal(5,2)");
        builder.Property(tm => tm.Feedback).HasColumnType("text");

        // Relationships
        builder.HasMany(tm => tm.MilestoneQuestions)
               .WithOne(mq => mq.TeamMilestone)
               .HasForeignKey(mq => mq.TeamMilestoneId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}