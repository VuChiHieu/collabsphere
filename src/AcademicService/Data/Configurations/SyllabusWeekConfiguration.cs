using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AcademicService.Models.Entities;

namespace AcademicService.Data.Configurations;

public class SyllabusWeekConfiguration : IEntityTypeConfiguration<SyllabusWeek>
{
    public void Configure(EntityTypeBuilder<SyllabusWeek> builder)
    {
        builder.HasKey(sw => sw.SyllabusWeekId);

        builder.HasIndex(sw => sw.SyllabusId);
        builder.HasIndex(sw => sw.WeekNumber);
        builder.HasIndex(sw => new { sw.SyllabusId, sw.WeekNumber }).IsUnique();

        builder.Property(sw => sw.Topic).IsRequired().HasMaxLength(500);
        builder.Property(sw => sw.Description).HasColumnType("text");
        builder.Property(sw => sw.LearningObjectives).HasColumnType("text");
        builder.Property(sw => sw.Activities).HasColumnType("text");
        builder.Property(sw => sw.Materials).HasColumnType("text");
    }
}