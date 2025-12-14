using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationService.Models.Entities;

namespace NotificationService.Data.Configurations;

public class NotificationQueueConfiguration : IEntityTypeConfiguration<NotificationQueue>
{
    public void Configure(EntityTypeBuilder<NotificationQueue> builder)
    {
        builder.HasKey(nq => nq.QueueId);

        builder.HasIndex(nq => nq.NotificationType);
        builder.HasIndex(nq => nq.Status);
        builder.HasIndex(nq => nq.ScheduledFor);
        builder.HasIndex(nq => nq.Priority);

        builder.Property(nq => nq.NotificationType).IsRequired().HasMaxLength(50).HasDefaultValue("EMAIL");
        builder.Property(nq => nq.Payload).IsRequired().HasColumnType("text");
        builder.Property(nq => nq.Status).HasMaxLength(50).HasDefaultValue("PENDING");
        builder.Property(nq => nq.RetryCount).HasDefaultValue(0);
        builder.Property(nq => nq.MaxRetries).HasDefaultValue(3);
        builder.Property(nq => nq.ErrorMessage).HasColumnType("text");
        builder.Property(nq => nq.Priority).HasMaxLength(50).HasDefaultValue("NORMAL");
    }
}