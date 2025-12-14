using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationService.Models.Entities;

namespace NotificationService.Data.Configurations;

public class NotificationPreferenceConfiguration : IEntityTypeConfiguration<NotificationPreference>
{
    public void Configure(EntityTypeBuilder<NotificationPreference> builder)
    {
        builder.HasKey(np => np.PreferenceId);

        builder.HasIndex(np => np.UserId).IsUnique(); // One preference per user

        builder.Property(np => np.EmailEnabled).HasDefaultValue(true);
        builder.Property(np => np.InAppEnabled).HasDefaultValue(true);
        builder.Property(np => np.PushEnabled).HasDefaultValue(false);
        builder.Property(np => np.NotificationTypes).HasColumnType("text");
        builder.Property(np => np.EnableQuietHours).HasDefaultValue(false);
        builder.Property(np => np.EnableDailyDigest).HasDefaultValue(false);
        builder.Property(np => np.EnableWeeklyDigest).HasDefaultValue(false);
        builder.Property(np => np.DigestTime).HasMaxLength(10);
        builder.Property(np => np.TimeZone).HasMaxLength(20);
    }
}