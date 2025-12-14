using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamService.Models.Entities;

namespace TeamService.Data.Configurations;

public class MilestoneQuestionConfiguration : IEntityTypeConfiguration<MilestoneQuestion>
{
    public void Configure(EntityTypeBuilder<MilestoneQuestion> builder)
    {
        builder.HasKey(mq => mq.QuestionId);

        // Indexes
        builder.HasIndex(mq => mq.TeamMilestoneId);
        builder.HasIndex(mq => mq.Order);

        // Properties
        builder.Property(mq => mq.QuestionText).IsRequired().HasColumnType("text");
        builder.Property(mq => mq.QuestionType).IsRequired().HasMaxLength(50).HasDefaultValue("TEXT");
        builder.Property(mq => mq.Options).HasColumnType("text");
        builder.Property(mq => mq.IsRequired).HasDefaultValue(true);
        builder.Property(mq => mq.MaxScore).HasColumnType("decimal(5,2)").HasDefaultValue(10);

        // Relationships
        builder.HasMany(mq => mq.MilestoneAnswers)
               .WithOne(ma => ma.MilestoneQuestion)
               .HasForeignKey(ma => ma.QuestionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}