using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectService.Models.Entities;

namespace ProjectService.Data.Configurations;

public class ClassProjectConfiguration : IEntityTypeConfiguration<ClassProject>
{
    public void Configure(EntityTypeBuilder<ClassProject> builder)
    {
        builder.HasKey(cp => cp.ClassProjectId);

        builder.HasIndex(cp => cp.ClassId);
        builder.HasIndex(cp => cp.ProjectId);
        builder.HasIndex(cp => new { cp.ClassId, cp.ProjectId }).IsUnique();

        builder.Property(cp => cp.AssignedAt).HasDefaultValueSql("NOW()");
        builder.Property(cp => cp.IsActive).HasDefaultValue(true);
    }
}