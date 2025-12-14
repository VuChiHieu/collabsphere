using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Models.Entities;

namespace UserService.Data.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(d => d.DepartmentId);

        builder.HasIndex(d => d.DepartmentName).IsUnique();
        builder.HasIndex(d => d.DepartmentCode).IsUnique();
        builder.HasIndex(d => d.HeadOfDepartmentId);

        builder.Property(d => d.DepartmentName).IsRequired().HasMaxLength(200);
        builder.Property(d => d.DepartmentCode).IsRequired().HasMaxLength(50);
        builder.Property(d => d.Description).HasColumnType("text");
        builder.Property(d => d.IsActive).HasDefaultValue(true);
    }
}