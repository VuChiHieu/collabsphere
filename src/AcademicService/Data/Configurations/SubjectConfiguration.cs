using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AcademicService.Models.Entities;

namespace AcademicService.Data.Configurations;

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.HasKey(s => s.SubjectId);

        builder.HasIndex(s => s.SubjectCode).IsUnique();
        builder.HasIndex(s => s.IsActive);
        builder.HasIndex(s => s.DepartmentId);

        builder.Property(s => s.SubjectCode).IsRequired().HasMaxLength(50);
        builder.Property(s => s.SubjectName).IsRequired().HasMaxLength(300);
        builder.Property(s => s.Description).HasColumnType("text");
        builder.Property(s => s.PrerequisiteSubjects).HasColumnType("text");
        builder.Property(s => s.IsActive).HasDefaultValue(true);

        builder.HasMany(s => s.Syllabi)
               .WithOne(sy => sy.Subject)
               .HasForeignKey(sy => sy.SubjectId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}