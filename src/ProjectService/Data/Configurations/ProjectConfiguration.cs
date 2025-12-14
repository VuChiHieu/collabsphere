using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectService.Models.Entities;

namespace ProjectService.Data.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.ProjectId);

        builder.HasIndex(p => p.ProjectCode).IsUnique();
        builder.HasIndex(p => p.SubjectId);
        builder.HasIndex(p => p.SyllabusId);
        builder.HasIndex(p => p.Status);
        builder.HasIndex(p => p.CreatedBy);

        builder.Property(p => p.ProjectCode).IsRequired().HasMaxLength(50);
        builder.Property(p => p.ProjectName).IsRequired().HasMaxLength(300);
        builder.Property(p => p.Description).IsRequired().HasColumnType("text");
        builder.Property(p => p.Overview).HasColumnType("text");
        builder.Property(p => p.Requirements).HasColumnType("text");
        builder.Property(p => p.ExpectedOutcomes).HasColumnType("text");
        builder.Property(p => p.TechnicalStack).HasColumnType("text");
        builder.Property(p => p.DifficultyLevel).HasMaxLength(50).HasDefaultValue("MEDIUM");
        builder.Property(p => p.Status).HasMaxLength(50).HasDefaultValue("DRAFT");
        builder.Property(p => p.RejectionReason).HasColumnType("text");
        builder.Property(p => p.IsActive).HasDefaultValue(true);
        builder.Property(p => p.MinTeamSize).HasDefaultValue(4);
        builder.Property(p => p.MaxTeamSize).HasDefaultValue(6);

        builder.HasMany(p => p.ProjectObjectives)
               .WithOne(po => po.Project)
               .HasForeignKey(po => po.ProjectId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.ProjectMilestones)
               .WithOne(pm => pm.Project)
               .HasForeignKey(pm => pm.ProjectId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.ClassProjects)
               .WithOne(cp => cp.Project)
               .HasForeignKey(cp => cp.ProjectId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.ProjectAIGenerations)
               .WithOne(pai => pai.Project)
               .HasForeignKey(pai => pai.ProjectId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}