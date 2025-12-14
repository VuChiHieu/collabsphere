using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AcademicService.Models.Entities;

namespace AcademicService.Data.Configurations;

public class ClassScheduleSlotConfiguration : IEntityTypeConfiguration<ClassScheduleSlot>
{
    public void Configure(EntityTypeBuilder<ClassScheduleSlot> builder)
    {
        builder.HasKey(css => css.ScheduleSlotId);

        builder.HasIndex(css => css.ClassId);
        builder.HasIndex(css => css.ScheduledDate);
        builder.HasIndex(css => new { css.ClassId, css.SlotNumber }).IsUnique();

        builder.Property(css => css.Room).HasMaxLength(50);
        builder.Property(css => css.Topic).HasMaxLength(500);
        builder.Property(css => css.Status).HasMaxLength(50).HasDefaultValue("SCHEDULED");
        builder.Property(css => css.Notes).HasColumnType("text");
    }
}