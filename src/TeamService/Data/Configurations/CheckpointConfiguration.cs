using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamService.Models.Entities;

namespace TeamService.Data.Configurations;

public class CheckpointConfiguration : IEntityTypeConfiguration<Checkpoint>
{
    public void Configure(EntityTypeBuilder<Checkpoint> builder)
    {
        builder.HasKey(c => c.CheckpointId);

        // Indexes
        builder.HasIndex(c => c.TeamId);
        builder.HasIndex(c => c.Status);
        builder.HasIndex(c => c.DueDate);

        // Properties
        builder.Property(c => c.CheckpointName).IsRequired().HasMaxLength(200);
        builder.Property(c => c.Description).HasColumnType("text");
        builder.Property(c => c.Status).HasMaxLength(50).HasDefaultValue("PENDING");
        builder.Property(c => c.Score).HasColumnType("decimal(5,2)");
        builder.Property(c => c.Feedback).HasColumnType("text");

        // Relationships
        builder.HasMany(c => c.CheckpointAssignments)
               .WithOne(ca => ca.Checkpoint)
               .HasForeignKey(ca => ca.CheckpointId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.CheckpointSubmissions)
               .WithOne(cs => cs.Checkpoint)
               .HasForeignKey(cs => cs.CheckpointId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}