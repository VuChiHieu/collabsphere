using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CommunicationService.Models.Entities;

namespace CommunicationService.Data.Configurations;

public class MeetingParticipantConfiguration : IEntityTypeConfiguration<MeetingParticipant>
{
    public void Configure(EntityTypeBuilder<MeetingParticipant> builder)
    {
        builder.HasKey(mp => mp.ParticipantId);

        builder.HasIndex(mp => mp.MeetingId);
        builder.HasIndex(mp => mp.UserId);
        builder.HasIndex(mp => new { mp.MeetingId, mp.UserId }).IsUnique();

        builder.Property(mp => mp.Role).HasMaxLength(50).HasDefaultValue("ATTENDEE");
        builder.Property(mp => mp.Status).HasMaxLength(50).HasDefaultValue("INVITED");
        builder.Property(mp => mp.HasAttended).HasDefaultValue(false);
    }
}