using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamService.Models.Entities;

namespace TeamService.Data.Configurations;

public class CheckpointSubmissionConfiguration : IEntityTypeConfiguration<CheckpointSubmission>
{
    public void Configure(EntityTypeBuilder<CheckpointSubmission> builder)
    {
        builder.HasKey(cs => cs.SubmissionId);

        // Indexes
        builder.HasIndex(cs => cs.CheckpointId);
        builder.HasIndex(cs => cs.SubmittedBy);
        builder.HasIndex(cs => cs.Version);
        builder.HasIndex(cs => cs.IsLatest);

        // Properties
        builder.Property(cs => cs.SubmissionFiles).HasColumnType("text");
        builder.Property(cs => cs.SubmissionNotes).HasColumnType("text");
        builder.Property(cs => cs.Version).HasDefaultValue(1);
        builder.Property(cs => cs.SubmittedAt).HasDefaultValueSql("NOW()");
        builder.Property(cs => cs.IsLatest).HasDefaultValue(true);
    }
}