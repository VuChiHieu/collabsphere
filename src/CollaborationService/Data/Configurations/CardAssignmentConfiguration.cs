using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CollaborationService.Models.Entities;

namespace CollaborationService.Data.Configurations;

public class CardAssignmentConfiguration : IEntityTypeConfiguration<CardAssignment>
{
    public void Configure(EntityTypeBuilder<CardAssignment> builder)
    {
        builder.HasKey(ca => ca.AssignmentId);

        // Indexes
        builder.HasIndex(ca => ca.CardId);
        builder.HasIndex(ca => ca.AssigneeId);
        builder.HasIndex(ca => new { ca.CardId, ca.AssigneeId }).IsUnique();

        // Properties
        builder.Property(ca => ca.AssignedAt).HasDefaultValueSql("NOW()");
        builder.Property(ca => ca.IsActive).HasDefaultValue(true);
    }
}