using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Models.Entities;

namespace UserService.Data.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(rt => rt.TokenId);

        builder.HasIndex(rt => rt.UserId);
        builder.HasIndex(rt => rt.Token).IsUnique();
        builder.HasIndex(rt => rt.ExpiresAt);

        builder.Property(rt => rt.Token).IsRequired().HasMaxLength(500);
        builder.Property(rt => rt.DeviceInfo).HasMaxLength(500);
        builder.Property(rt => rt.IsRevoked).HasDefaultValue(false);
    }
}