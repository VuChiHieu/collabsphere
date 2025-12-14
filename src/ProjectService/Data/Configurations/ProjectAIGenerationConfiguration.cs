using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectService.Models.Entities;

namespace ProjectService.Data.Configurations;

public class ProjectAIGenerationConfiguration : IEntityTypeConfiguration<ProjectAIGeneration>
{
    public void Configure(EntityTypeBuilder<ProjectAIGeneration> builder)
    {
        builder.HasKey(pai => pai.GenerationId);

        builder.HasIndex(pai => pai.ProjectId);
        builder.HasIndex(pai => pai.CreatedAt);

        builder.Property(pai => pai.PromptUsed).IsRequired().HasColumnType("text");
        builder.Property(pai => pai.GeneratedContent).IsRequired().HasColumnType("text");
        builder.Property(pai => pai.Model).HasMaxLength(50).HasDefaultValue("gemini-pro");
    }
}