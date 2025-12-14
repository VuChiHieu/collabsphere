using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Models.Entities;

namespace UserService.Data.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(ur => ur.UserRoleId);

        // Composite unique index
        builder.HasIndex(ur => new { ur.UserId, ur.RoleId }).IsUnique();

        builder.Property(ur => ur.AssignedAt).HasDefaultValueSql("NOW()");

        // Relationships already defined in User and Role configurations
    }
}