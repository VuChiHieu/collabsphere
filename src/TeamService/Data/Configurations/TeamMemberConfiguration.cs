using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamService.Models.Entities;

namespace TeamService.Data.Configurations;

public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
{
    public void Configure(EntityTypeBuilder<TeamMember> builder)
    {
        builder.HasKey(tm => tm.TeamMemberId);

        // Indexes
        builder.HasIndex(tm => tm.TeamId);
        builder.HasIndex(tm => tm.StudentId);
        builder.HasIndex(tm => new { tm.TeamId, tm.StudentId }).IsUnique();
        builder.HasIndex(tm => tm.Status);

        // Properties
        builder.Property(tm => tm.Role).HasMaxLength(50).HasDefaultValue("MEMBER");
        builder.Property(tm => tm.ContributionPercentage).HasColumnType("decimal(5,2)").HasDefaultValue(0);
        builder.Property(tm => tm.JoinedAt).HasDefaultValueSql("NOW()");
        builder.Property(tm => tm.Status).HasMaxLength(50).HasDefaultValue("ACTIVE");
        builder.Property(tm => tm.IsActive).HasDefaultValue(true);

        // Relationships
        builder.HasMany(tm => tm.MilestoneAnswers)
               .WithOne(ma => ma.TeamMember)
               .HasForeignKey(ma => ma.TeamMemberId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(tm => tm.CheckpointAssignments)
               .WithOne(ca => ca.TeamMember)
               .HasForeignKey(ca => ca.TeamMemberId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}