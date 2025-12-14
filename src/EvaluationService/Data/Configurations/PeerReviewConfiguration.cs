using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EvaluationService.Models.Entities;

namespace EvaluationService.Data.Configurations;

public class PeerReviewConfiguration : IEntityTypeConfiguration<PeerReview>
{
    public void Configure(EntityTypeBuilder<PeerReview> builder)
    {
        builder.HasKey(pr => pr.ReviewId);

        builder.HasIndex(pr => pr.TeamId);
        builder.HasIndex(pr => pr.ReviewerId);
        builder.HasIndex(pr => pr.RevieweeId);
        builder.HasIndex(pr => pr.MilestoneId);
        builder.HasIndex(pr => new { pr.TeamId, pr.ReviewerId, pr.RevieweeId, pr.MilestoneId }).IsUnique();

        builder.Property(pr => pr.TeamworkScore).HasColumnType("decimal(5,2)").IsRequired();
        builder.Property(pr => pr.CommunicationScore).HasColumnType("decimal(5,2)").IsRequired();
        builder.Property(pr => pr.TechnicalSkillScore).HasColumnType("decimal(5,2)").IsRequired();
        builder.Property(pr => pr.ContributionScore).HasColumnType("decimal(5,2)").IsRequired();
        builder.Property(pr => pr.OverallScore).HasColumnType("decimal(5,2)").IsRequired();
        builder.Property(pr => pr.Strengths).HasColumnType("text");
        builder.Property(pr => pr.AreasForImprovement).HasColumnType("text");
        builder.Property(pr => pr.AdditionalComments).HasColumnType("text");
        builder.Property(pr => pr.IsAnonymous).HasDefaultValue(true);
        builder.Property(pr => pr.ReviewedAt).HasDefaultValueSql("NOW()");
    }
}