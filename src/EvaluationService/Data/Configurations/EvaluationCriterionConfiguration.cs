using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EvaluationService.Models.Entities;

namespace EvaluationService.Data.Configurations;

public class EvaluationCriterionConfiguration : IEntityTypeConfiguration<EvaluationCriterion>
{
    public void Configure(EntityTypeBuilder<EvaluationCriterion> builder)
    {
        builder.HasKey(ec => ec.CriterionId);

        builder.HasIndex(ec => ec.EvaluationType);
        builder.HasIndex(ec => ec.Order);
        builder.HasIndex(ec => ec.IsActive);

        builder.Property(ec => ec.EvaluationType).IsRequired().HasMaxLength(50).HasDefaultValue("GENERAL");
        builder.Property(ec => ec.CriterionName).IsRequired().HasMaxLength(200);
        builder.Property(ec => ec.Description).HasColumnType("text");
        builder.Property(ec => ec.MaxScore).HasColumnType("decimal(5,2)").HasDefaultValue(10);
        builder.Property(ec => ec.Weight).HasColumnType("decimal(5,2)").HasDefaultValue(1);
        builder.Property(ec => ec.Order).HasDefaultValue(0);
        builder.Property(ec => ec.IsActive).HasDefaultValue(true);
    }
}