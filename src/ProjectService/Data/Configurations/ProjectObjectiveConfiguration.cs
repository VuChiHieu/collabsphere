using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectService.Models.Entities;

namespace ProjectService.Data.Configurations;

public class ProjectObjectiveConfiguration : IEntityTypeConfiguration<ProjectObjective>
{
    public void Configure(EntityTypeBuilder<ProjectObjective> builder)
    {
        builder.HasKey(po => po.ObjectiveId);

        builder.HasIndex(po => po.ProjectId);
        builder.HasIndex(po => new { po.ProjectId, po.ObjectiveCode }).IsUnique();

        builder.Property(po => po.ObjectiveCode).IsRequired().HasMaxLength(50);
        builder.Property(po => po.Title).IsRequired().HasMaxLength(300);
        builder.Property(po => po.Description).IsRequired().HasColumnType("text");
        builder.Property(po => po.BloomLevel).HasMaxLength(50);
        builder.Property(po => po.Weight).HasColumnType("decimal(5,2)").HasDefaultValue(0);
    }
}