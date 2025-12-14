using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EvaluationService.Models.Entities;

namespace EvaluationService.Data.Configurations;

public class MilestoneAnswerEvaluationConfiguration : IEntityTypeConfiguration<MilestoneAnswerEvaluation>
{
    public void Configure(EntityTypeBuilder<MilestoneAnswerEvaluation> builder)
    {
        builder.HasKey(mae => mae.EvaluationId);

        builder.HasIndex(mae => mae.AnswerId).IsUnique();
        builder.HasIndex(mae => mae.EvaluatedBy);
        builder.HasIndex(mae => mae.Status);

        builder.Property(mae => mae.Score).HasColumnType("decimal(5,2)").IsRequired();
        builder.Property(mae => mae.Feedback).HasColumnType("text");
        builder.Property(mae => mae.RevisionNotes).HasColumnType("text");
        builder.Property(mae => mae.IsApproved).HasDefaultValue(false);
        builder.Property(mae => mae.Status).HasMaxLength(50).HasDefaultValue("PENDING");
        builder.Property(mae => mae.EvaluatedAt).HasDefaultValueSql("NOW()");
    }
}