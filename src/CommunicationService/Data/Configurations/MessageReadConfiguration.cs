using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CommunicationService.Models.Entities;

namespace CommunicationService.Data.Configurations;

public class MessageReadConfiguration : IEntityTypeConfiguration<MessageRead>
{
    public void Configure(EntityTypeBuilder<MessageRead> builder)
    {
        builder.HasKey(mr => mr.ReadId);

        builder.HasIndex(mr => mr.MessageId);
        builder.HasIndex(mr => mr.UserId);
        builder.HasIndex(mr => new { mr.MessageId, mr.UserId }).IsUnique();

        builder.Property(mr => mr.ReadAt).HasDefaultValueSql("NOW()");
    }
}