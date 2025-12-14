using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EvaluationService.Models.Entities;

namespace EvaluationService.Data.Configurations;

public class TeamEvaluationConfiguration : IEntityTypeConfiguration<TeamEvaluation>
{
    public void Configure(EntityTypeBuilder<TeamEvaluation> builder)
    {
        builder.HasKey(te => te.EvaluationId);

        builder.HasIndex(te => te.TeamId);
        builder.HasIndex(te => te.EvaluatedBy);
        builder.HasIndex(te => te.EvaluationType);
        builder.HasIndex(te => te.MilestoneId);

        builder.Property(te => te.OverallScore).HasColumnType("decimal(5,2)").IsRequired();
        builder.Property(te => te.Feedback).HasColumnType("text");
        builder.Property(te => te.Criteria).HasColumnType("text");
        builder.Property(te => te.EvaluatedAt).HasDefaultValueSql("NOW()");
        builder.Property(te => te.EvaluationType).HasMaxLength(50).HasDefaultValue("FINAL");
    }
}