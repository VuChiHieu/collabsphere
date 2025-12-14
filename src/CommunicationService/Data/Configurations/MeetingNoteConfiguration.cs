using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CommunicationService.Models.Entities;

namespace CommunicationService.Data.Configurations;

public class MeetingNoteConfiguration : IEntityTypeConfiguration<MeetingNote>
{
    public void Configure(EntityTypeBuilder<MeetingNote> builder)
    {
        builder.HasKey(mn => mn.NoteId);

        builder.HasIndex(mn => mn.MeetingId);
        builder.HasIndex(mn => mn.CreatedBy);

        builder.Property(mn => mn.NoteContent).IsRequired().HasColumnType("text");
        builder.Property(mn => mn.IsShared).HasDefaultValue(false);

        builder.HasQueryFilter(mn => !mn.IsDeleted);
    }
}