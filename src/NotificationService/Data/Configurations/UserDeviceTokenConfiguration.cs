using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationService.Models.Entities;

namespace NotificationService.Data.Configurations;

public class UserDeviceTokenConfiguration : IEntityTypeConfiguration<UserDeviceToken>
{
    public void Configure(EntityTypeBuilder<UserDeviceToken> builder)
    {
        builder.HasKey(udt => udt.TokenId);

        builder.HasIndex(udt => udt.UserId);
        builder.HasIndex(udt => udt.DeviceToken).IsUnique();
        builder.HasIndex(udt => udt.IsActive);

        builder.Property(udt => udt.DeviceToken).IsRequired().HasMaxLength(500);
        builder.Property(udt => udt.DeviceType).IsRequired().HasMaxLength(50).HasDefaultValue("WEB");
        builder.Property(udt => udt.DeviceName).HasMaxLength(100);
        builder.Property(udt => udt.IsActive).HasDefaultValue(true);
    }
}