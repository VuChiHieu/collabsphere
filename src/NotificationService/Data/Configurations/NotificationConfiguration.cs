using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationService.Models.Entities;

namespace NotificationService.Data.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(n => n.NotificationId);

        builder.HasIndex(n => n.UserId);
        builder.HasIndex(n => n.NotificationType);
        builder.HasIndex(n => n.Priority);
        builder.HasIndex(n => n.IsRead);
        builder.HasIndex(n => n.CreatedAt);

        builder.Property(n => n.NotificationType).IsRequired().HasMaxLength(50).HasDefaultValue("INFO");
        builder.Property(n => n.Title).IsRequired().HasMaxLength(300);
        builder.Property(n => n.Message).IsRequired().HasColumnType("text");
        builder.Property(n => n.Priority).HasMaxLength(50).HasDefaultValue("NORMAL");
        builder.Property(n => n.ActionUrl).HasMaxLength(500);
        builder.Property(n => n.Metadata).HasColumnType("text");
        builder.Property(n => n.IsRead).HasDefaultValue(false);
        builder.Property(n => n.IsDismissed).HasDefaultValue(false);

        builder.HasQueryFilter(n => !n.IsDeleted);
    }
}