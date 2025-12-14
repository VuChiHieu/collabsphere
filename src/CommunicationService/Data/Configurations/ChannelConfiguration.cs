using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CommunicationService.Models.Entities;

namespace CommunicationService.Data.Configurations;

public class ChannelConfiguration : IEntityTypeConfiguration<Channel>
{
    public void Configure(EntityTypeBuilder<Channel> builder)
    {
        builder.HasKey(c => c.ChannelId);

        builder.HasIndex(c => c.TeamId);
        builder.HasIndex(c => c.ChannelType);
        builder.HasIndex(c => c.IsPrivate);
        builder.HasIndex(c => c.IsDefault);

        builder.Property(c => c.ChannelName).IsRequired().HasMaxLength(200);
        builder.Property(c => c.Description).HasColumnType("text");
        builder.Property(c => c.ChannelType).HasMaxLength(50).HasDefaultValue("PUBLIC");
        builder.Property(c => c.IsPrivate).HasDefaultValue(false);
        builder.Property(c => c.IsDefault).HasDefaultValue(false);
        builder.Property(c => c.MemberCount).HasDefaultValue(0);
        builder.Property(c => c.AvatarUrl).HasMaxLength(500);

        builder.HasQueryFilter(c => !c.IsDeleted);

        builder.HasMany(c => c.ChannelMembers)
                .WithOne(cm => cm.Channel)
                .HasForeignKey(cm => cm.ChannelId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Messages)
                .WithOne(m => m.Channel)
                .HasForeignKey(m => m.ChannelId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Meetings)
                .WithOne(m => m.Channel)
                .HasForeignKey(m => m.ChannelId)
                .OnDelete(DeleteBehavior.SetNull);
    }
}