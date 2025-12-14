using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Models.Entities;

namespace UserService.Data.Configurations;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasKey(p => p.ProfileId);

        builder.HasIndex(p => p.UserId).IsUnique();
        builder.HasIndex(p => p.StudentCode);
        builder.HasIndex(p => p.LecturerCode);
        builder.HasIndex(p => p.StaffCode);

        builder.Property(p => p.StudentCode).HasMaxLength(50);
        builder.Property(p => p.LecturerCode).HasMaxLength(50);
        builder.Property(p => p.StaffCode).HasMaxLength(50);
        builder.Property(p => p.Gender).HasMaxLength(10);
        builder.Property(p => p.Address).HasMaxLength(500);
        builder.Property(p => p.Bio).HasColumnType("text");

        builder.HasOne(p => p.Department)
               .WithMany(d => d.UserProfiles)
               .HasForeignKey(p => p.DepartmentId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}