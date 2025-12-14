using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AcademicService.Models.Entities;

namespace AcademicService.Data.Configurations;

public class ClassConfiguration : IEntityTypeConfiguration<Class>
{
    public void Configure(EntityTypeBuilder<Class> builder)
    {
        builder.HasKey(c => c.ClassId);

        builder.HasIndex(c => c.SyllabusId);
        builder.HasIndex(c => c.ClassCode).IsUnique();
        builder.HasIndex(c => new { c.Semester, c.AcademicYear });
        builder.HasIndex(c => c.Status);

        builder.Property(c => c.ClassCode).IsRequired().HasMaxLength(50);
        builder.Property(c => c.ClassName).IsRequired().HasMaxLength(200);
        builder.Property(c => c.Semester).IsRequired().HasMaxLength(50);
        builder.Property(c => c.AcademicYear).IsRequired().HasMaxLength(20);
        builder.Property(c => c.Schedule).HasColumnType("text");
        builder.Property(c => c.Room).HasMaxLength(50);
        builder.Property(c => c.Status).HasMaxLength(50).HasDefaultValue("PLANNED");
        builder.Property(c => c.IsActive).HasDefaultValue(true);
        builder.Property(c => c.MaxStudents).HasDefaultValue(35);
        builder.Property(c => c.CurrentStudents).HasDefaultValue(0);

        builder.HasMany(c => c.ClassLecturers)
               .WithOne(cl => cl.Class)
               .HasForeignKey(cl => cl.ClassId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.ClassStudents)
               .WithOne(cs => cs.Class)
               .HasForeignKey(cs => cs.ClassId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.ClassScheduleSlots)
               .WithOne(css => css.Class)
               .HasForeignKey(css => css.ClassId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}