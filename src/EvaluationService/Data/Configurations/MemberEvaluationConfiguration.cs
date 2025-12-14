using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EvaluationService.Models.Entities;

namespace EvaluationService.Data.Configurations;

public class MemberEvaluationConfiguration : IEntityTypeConfiguration<MemberEvaluation>
{
    public void Configure(EntityTypeBuilder<MemberEvaluation> builder)
    {
        builder.HasKey(me => me.EvaluationId);

        builder.HasIndex(me => me.TeamId);
        builder.HasIndex(me => me.StudentId);
        builder.HasIndex(me => me.EvaluatedBy);
        builder.HasIndex(me => me.EvaluationType);

        builder.Property(me => me.Score).HasColumnType("decimal(5,2)").IsRequired();
        builder.Property(me => me.ContributionScore).HasColumnType("decimal(5,2)");
        builder.Property(me => me.Feedback).HasColumnType("text");
        builder.Property(me => me.Criteria).HasColumnType("text");
        builder.Property(me => me.EvaluatedAt).HasDefaultValueSql("NOW()");
        builder.Property(me => me.EvaluationType).HasMaxLength(50).HasDefaultValue("FINAL");
    }
}