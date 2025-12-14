using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamService.Models.Entities;

namespace TeamService.Data.Configurations;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasKey(t => t.TeamId);

        // Indexes
        builder.HasIndex(t => t.TeamCode).IsUnique();
        builder.HasIndex(t => t.ClassId);
        builder.HasIndex(t => t.ProjectId);
        builder.HasIndex(t => t.Status);
        builder.HasIndex(t => t.TeamLeaderId);

        // Properties
        builder.Property(t => t.TeamCode).IsRequired().HasMaxLength(50);
        builder.Property(t => t.TeamName).IsRequired().HasMaxLength(200);
        builder.Property(t => t.Status).HasMaxLength(50).HasDefaultValue("FORMING");
        builder.Property(t => t.OverallProgress).HasColumnType("decimal(5,2)").HasDefaultValue(0);
        builder.Property(t => t.CurrentMembers).HasDefaultValue(0);
        builder.Property(t => t.IsActive).HasDefaultValue(true);

        // Relationships
        builder.HasMany(t => t.TeamMembers)
               .WithOne(tm => tm.Team)
               .HasForeignKey(tm => tm.TeamId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.TeamMilestones)
               .WithOne(tm => tm.Team)
               .HasForeignKey(tm => tm.TeamId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Checkpoints)
               .WithOne(c => c.Team)
               .HasForeignKey(c => c.TeamId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.TeamProgressLogs)
               .WithOne(tpl => tpl.Team)
               .HasForeignKey(tpl => tpl.TeamId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}