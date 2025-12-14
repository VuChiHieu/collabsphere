using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CommunicationService.Models.Entities;

namespace CommunicationService.Data.Configurations;

public class ChannelMemberConfiguration : IEntityTypeConfiguration<ChannelMember>
{
    public void Configure(EntityTypeBuilder<ChannelMember> builder)
    {
        builder.HasKey(cm => cm.MemberId);

        builder.HasIndex(cm => cm.ChannelId);
        builder.HasIndex(cm => cm.UserId);
        builder.HasIndex(cm => new { cm.ChannelId, cm.UserId }).IsUnique();

        builder.Property(cm => cm.Role).HasMaxLength(50).HasDefaultValue("MEMBER");
        builder.Property(cm => cm.JoinedAt).HasDefaultValueSql("NOW()");
        builder.Property(cm => cm.IsMuted).HasDefaultValue(false);
        builder.Property(cm => cm.IsActive).HasDefaultValue(true);
    }
}