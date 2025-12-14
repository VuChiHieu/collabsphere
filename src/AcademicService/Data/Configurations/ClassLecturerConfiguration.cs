using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AcademicService.Models.Entities;

namespace AcademicService.Data.Configurations;

public class ClassLecturerConfiguration : IEntityTypeConfiguration<ClassLecturer>
{
    public void Configure(EntityTypeBuilder<ClassLecturer> builder)
    {
        builder.HasKey(cl => cl.ClassLecturerId);

        builder.HasIndex(cl => cl.ClassId);
        builder.HasIndex(cl => cl.LecturerId);
        builder.HasIndex(cl => new { cl.ClassId, cl.LecturerId }).IsUnique();

        builder.Property(cl => cl.Role).HasMaxLength(50).HasDefaultValue("PRIMARY");
        builder.Property(cl => cl.IsActive).HasDefaultValue(true);
        builder.Property(cl => cl.AssignedAt).HasDefaultValueSql("NOW()");
    }
}