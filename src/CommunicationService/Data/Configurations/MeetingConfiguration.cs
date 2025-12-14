using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CommunicationService.Models.Entities;

namespace CommunicationService.Data.Configurations;

public class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
{
    public void Configure(EntityTypeBuilder<Meeting> builder)
    {
        builder.HasKey(m => m.MeetingId);

        builder.HasIndex(m => m.TeamId);
        builder.HasIndex(m => m.ChannelId);
        builder.HasIndex(m => m.Status);
        builder.HasIndex(m => m.ScheduledStartTime);

        builder.Property(m => m.MeetingTitle).IsRequired().HasMaxLength(300);
        builder.Property(m => m.Description).HasColumnType("text");
        builder.Property(m => m.MeetingType).HasMaxLength(50).HasDefaultValue("VIDEO");
        builder.Property(m => m.Status).HasMaxLength(50).HasDefaultValue("SCHEDULED");
        builder.Property(m => m.MeetingUrl).HasMaxLength(500);
        builder.Property(m => m.Agenda).HasColumnType("text");
        builder.Property(m => m.RecordingUrl).HasMaxLength(500);

        builder.HasMany(m => m.MeetingParticipants)
                .WithOne(mp => mp.Meeting)
                .HasForeignKey(mp => mp.MeetingId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(m => m.MeetingNotes)
                .WithOne(mn => mn.Meeting)
                .HasForeignKey(mn => mn.MeetingId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}