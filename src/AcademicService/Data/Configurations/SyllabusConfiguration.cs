using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AcademicService.Models.Entities;

namespace AcademicService.Data.Configurations;

public class SyllabusConfiguration : IEntityTypeConfiguration<Syllabus>
{
    public void Configure(EntityTypeBuilder<Syllabus> builder)
    {
        builder.HasKey(s => s.SyllabusId);

        builder.HasIndex(s => s.SubjectId);
        builder.HasIndex(s => s.SyllabusCode).IsUnique();
        builder.HasIndex(s => new { s.Semester, s.AcademicYear });
        builder.HasIndex(s => s.IsActive);

        builder.Property(s => s.SyllabusCode).IsRequired().HasMaxLength(100);
        builder.Property(s => s.SyllabusName).IsRequired().HasMaxLength(300);
        builder.Property(s => s.Semester).IsRequired().HasMaxLength(50);
        builder.Property(s => s.AcademicYear).IsRequired().HasMaxLength(20);
        builder.Property(s => s.Description).HasColumnType("text");
        builder.Property(s => s.LearningObjectives).HasColumnType("text");
        builder.Property(s => s.CourseOutline).HasColumnType("text");
        builder.Property(s => s.AssessmentMethods).HasColumnType("text");
        builder.Property(s => s.RequiredMaterials).HasColumnType("text");
        builder.Property(s => s.TeachingMethods).HasColumnType("text");
        builder.Property(s => s.GradingScheme).HasColumnType("text");
        builder.Property(s => s.IsActive).HasDefaultValue(true);
        builder.Property(s => s.TotalSlots).HasDefaultValue(15);

        builder.HasMany(s => s.SyllabusWeeks)
               .WithOne(sw => sw.Syllabus)
               .HasForeignKey(sw => sw.SyllabusId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Classes)
               .WithOne(c => c.Syllabus)
               .HasForeignKey(c => c.SyllabusId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}