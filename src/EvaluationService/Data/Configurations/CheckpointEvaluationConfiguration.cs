using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EvaluationService.Models.Entities;

namespace EvaluationService.Data.Configurations;

public class CheckpointEvaluationConfiguration : IEntityTypeConfiguration<CheckpointEvaluation>
{
    public void Configure(EntityTypeBuilder<CheckpointEvaluation> builder)
    {
        builder.HasKey(ce => ce.EvaluationId);

        builder.HasIndex(ce => ce.CheckpointId).IsUnique();
        builder.HasIndex(ce => ce.EvaluatedBy);
        builder.HasIndex(ce => ce.Status);

        builder.Property(ce => ce.Score).HasColumnType("decimal(5,2)").IsRequired();
        builder.Property(ce => ce.Feedback).HasColumnType("text");
        builder.Property(ce => ce.Criteria).HasColumnType("text");
        builder.Property(ce => ce.RevisionNotes).HasColumnType("text");
        builder.Property(ce => ce.IsApproved).HasDefaultValue(false);
        builder.Property(ce => ce.Status).HasMaxLength(50).HasDefaultValue("PENDING");
        builder.Property(ce => ce.EvaluatedAt).HasDefaultValueSql("NOW()");
    }
}