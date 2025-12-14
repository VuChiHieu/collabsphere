using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CommunicationService.Models.Entities;

namespace CommunicationService.Data.Configurations;

public class DirectMessageConfiguration : IEntityTypeConfiguration<DirectMessage>
{
    public void Configure(EntityTypeBuilder<DirectMessage> builder)
    {
        builder.HasKey(dm => dm.MessageId);

        builder.HasIndex(dm => dm.SenderId);
        builder.HasIndex(dm => dm.ReceiverId);
        builder.HasIndex(dm => new { dm.SenderId, dm.ReceiverId });
        builder.HasIndex(dm => dm.IsRead);

        builder.Property(dm => dm.MessageText).IsRequired().HasColumnType("text");
        builder.Property(dm => dm.MessageType).HasMaxLength(50).HasDefaultValue("TEXT");
        builder.Property(dm => dm.Attachments).HasColumnType("text");
        builder.Property(dm => dm.IsRead).HasDefaultValue(false);
        builder.Property(dm => dm.IsEdited).HasDefaultValue(false);

        builder.HasQueryFilter(dm => !dm.IsDeleted);
    }
}