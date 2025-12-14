using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamService.Models.Entities;

namespace TeamService.Data.Configurations;

public class MilestoneAnswerConfiguration : IEntityTypeConfiguration<MilestoneAnswer>
{
    public void Configure(EntityTypeBuilder<MilestoneAnswer> builder)
    {
        builder.HasKey(ma => ma.AnswerId);

        // Indexes
        builder.HasIndex(ma => ma.QuestionId);
        builder.HasIndex(ma => ma.TeamMemberId);
        builder.HasIndex(ma => new { ma.QuestionId, ma.TeamMemberId }).IsUnique();

        // Properties
        builder.Property(ma => ma.AnswerText).HasColumnType("text");
        builder.Property(ma => ma.FileUrl).HasMaxLength(500);
        builder.Property(ma => ma.SubmittedAt).HasDefaultValueSql("NOW()");
        builder.Property(ma => ma.Score).HasColumnType("decimal(5,2)");
        builder.Property(ma => ma.Feedback).HasColumnType("text");
        builder.Property(ma => ma.IsApproved).HasDefaultValue(false);
    }
}