using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationService.Models.Entities;

namespace NotificationService.Data.Configurations;

public class EmailNotificationConfiguration : IEntityTypeConfiguration<EmailNotification>
{
    public void Configure(EntityTypeBuilder<EmailNotification> builder)
    {
        builder.HasKey(en => en.EmailId);

        builder.HasIndex(en => en.UserId);
        builder.HasIndex(en => en.EmailType);
        builder.HasIndex(en => en.Status);
        builder.HasIndex(en => en.SentAt);

        builder.Property(en => en.EmailType).IsRequired().HasMaxLength(50).HasDefaultValue("GENERAL");
        builder.Property(en => en.RecipientEmail).IsRequired().HasMaxLength(255);
        builder.Property(en => en.Subject).IsRequired().HasMaxLength(300);
        builder.Property(en => en.Body).IsRequired().HasColumnType("text");
        builder.Property(en => en.Status).HasMaxLength(50).HasDefaultValue("PENDING");
        builder.Property(en => en.RetryCount).HasDefaultValue(0);
        builder.Property(en => en.ErrorMessage).HasColumnType("text");
        builder.Property(en => en.Metadata).HasColumnType("text");
    }
}