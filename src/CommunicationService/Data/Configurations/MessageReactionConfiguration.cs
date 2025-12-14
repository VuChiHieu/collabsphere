using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CommunicationService.Models.Entities;

namespace CommunicationService.Data.Configurations;

public class MessageReactionConfiguration : IEntityTypeConfiguration<MessageReaction>
{
    public void Configure(EntityTypeBuilder<MessageReaction> builder)
    {
        builder.HasKey(mr => mr.ReactionId);

        builder.HasIndex(mr => mr.MessageId);
        builder.HasIndex(mr => mr.UserId);
        builder.HasIndex(mr => new { mr.MessageId, mr.UserId, mr.Emoji }).IsUnique();

        builder.Property(mr => mr.Emoji).IsRequired().HasMaxLength(10);
        builder.Property(mr => mr.ReactedAt).HasDefaultValueSql("NOW()");
    }
}