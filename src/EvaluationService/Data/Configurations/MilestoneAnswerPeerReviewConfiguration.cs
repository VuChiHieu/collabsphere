using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EvaluationService.Models.Entities;

namespace EvaluationService.Data.Configurations;

public class MilestoneAnswerPeerReviewConfiguration : IEntityTypeConfiguration<MilestoneAnswerPeerReview>
{
    public void Configure(EntityTypeBuilder<MilestoneAnswerPeerReview> builder)
    {
        builder.HasKey(mapr => mapr.ReviewId);

        builder.HasIndex(mapr => mapr.AnswerId);
        builder.HasIndex(mapr => mapr.ReviewerId);
        builder.HasIndex(mapr => new { mapr.AnswerId, mapr.ReviewerId }).IsUnique();

        builder.Property(mapr => mapr.Rating).HasColumnType("decimal(5,2)").IsRequired();
        builder.Property(mapr => mapr.Comment).HasColumnType("text");
        builder.Property(mapr => mapr.IsHelpful).HasDefaultValue(false);
        builder.Property(mapr => mapr.ReviewedAt).HasDefaultValueSql("NOW()");
    }
}