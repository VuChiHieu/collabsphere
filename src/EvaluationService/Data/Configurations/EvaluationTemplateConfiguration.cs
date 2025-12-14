using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EvaluationService.Models.Entities;

namespace EvaluationService.Data.Configurations;

public class EvaluationTemplateConfiguration : IEntityTypeConfiguration<EvaluationTemplate>
{
    public void Configure(EntityTypeBuilder<EvaluationTemplate> builder)
    {
        builder.HasKey(et => et.TemplateId);

        builder.HasIndex(et => et.EvaluationType);
        builder.HasIndex(et => et.IsPublic);
        builder.HasIndex(et => et.IsActive);

        builder.Property(et => et.TemplateName).IsRequired().HasMaxLength(200);
        builder.Property(et => et.Description).HasColumnType("text");
        builder.Property(et => et.EvaluationType).IsRequired().HasMaxLength(50).HasDefaultValue("GENERAL");
        builder.Property(et => et.Criteria).HasColumnType("text");
        builder.Property(et => et.IsPublic).HasDefaultValue(false);
        builder.Property(et => et.IsActive).HasDefaultValue(true);
    }
}