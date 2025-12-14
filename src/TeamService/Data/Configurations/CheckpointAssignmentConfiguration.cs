using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamService.Models.Entities;

namespace TeamService.Data.Configurations;

public class CheckpointAssignmentConfiguration : IEntityTypeConfiguration<CheckpointAssignment>
{
    public void Configure(EntityTypeBuilder<CheckpointAssignment> builder)
    {
        builder.HasKey(ca => ca.AssignmentId);

        // Indexes
        builder.HasIndex(ca => ca.CheckpointId);
        builder.HasIndex(ca => ca.TeamMemberId);
        builder.HasIndex(ca => new { ca.CheckpointId, ca.TeamMemberId }).IsUnique();
        builder.HasIndex(ca => ca.Status);

        // Properties
        builder.Property(ca => ca.AssignedAt).HasDefaultValueSql("NOW()");
        builder.Property(ca => ca.Status).HasMaxLength(50).HasDefaultValue("ASSIGNED");
    }
}