using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CommunicationService.Models.Entities;

namespace CommunicationService.Data.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(m => m.MessageId);

        builder.HasIndex(m => m.ChannelId);
        builder.HasIndex(m => m.SenderId);
        builder.HasIndex(m => m.ParentMessageId);
        builder.HasIndex(m => m.IsPinned);
        builder.HasIndex(m => m.CreatedAt);

        builder.Property(m => m.MessageText).IsRequired().HasColumnType("text");
        builder.Property(m => m.MessageType).HasMaxLength(50).HasDefaultValue("TEXT");
        builder.Property(m => m.Attachments).HasColumnType("text");
        builder.Property(m => m.IsPinned).HasDefaultValue(false);
        builder.Property(m => m.IsEdited).HasDefaultValue(false);

        builder.HasQueryFilter(m => !m.IsDeleted);

        builder.HasOne(m => m.ParentMessage)
                .WithMany(m => m.Replies)
                .HasForeignKey(m => m.ParentMessageId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m => m.MessageReactions)
                .WithOne(mr => mr.Message)
                .HasForeignKey(mr => mr.MessageId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(m => m.MessageReads)
                .WithOne(mr => mr.Message)
                .HasForeignKey(mr => mr.MessageId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}