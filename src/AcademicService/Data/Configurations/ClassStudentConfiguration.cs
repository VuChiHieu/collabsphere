using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AcademicService.Models.Entities;

namespace AcademicService.Data.Configurations;

public class ClassStudentConfiguration : IEntityTypeConfiguration<ClassStudent>
{
    public void Configure(EntityTypeBuilder<ClassStudent> builder)
    {
        builder.HasKey(cs => cs.ClassStudentId);

        builder.HasIndex(cs => cs.ClassId);
        builder.HasIndex(cs => cs.StudentId);
        builder.HasIndex(cs => cs.Status);
        builder.HasIndex(cs => new { cs.ClassId, cs.StudentId }).IsUnique();

        builder.Property(cs => cs.Status).HasMaxLength(50).HasDefaultValue("ENROLLED");
        builder.Property(cs => cs.FinalGrade).HasColumnType("decimal(5,2)");
        builder.Property(cs => cs.EnrollmentDate).HasDefaultValueSql("NOW()");
    }
}