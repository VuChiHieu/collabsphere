using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CollaborationService.Models.Entities;

namespace CollaborationService.Data.Configurations;

public class SprintConfiguration : IEntityTypeConfiguration<Sprint>
{
    public void Configure(EntityTypeBuilder<Sprint> builder)
    {
        builder.HasKey(s => s.SprintId);

        // Indexes
        builder.HasIndex(s => s.WorkspaceId);
        builder.HasIndex(s => s.Status);
        builder.HasIndex(s => s.StartDate);
        builder.HasIndex(s => s.EndDate);

        // Properties
        builder.Property(s => s.SprintName).IsRequired().HasMaxLength(200);
        builder.Property(s => s.Description).HasColumnType("text");
        builder.Property(s => s.Goals).HasColumnType("text");
        builder.Property(s => s.Status).HasMaxLength(50).HasDefaultValue("PLANNED");
        builder.Property(s => s.CompletionRate).HasColumnType("decimal(5,2)").HasDefaultValue(0);
        builder.Property(s => s.TotalStoryPoints).HasDefaultValue(0);
        builder.Property(s => s.CompletedStoryPoints).HasDefaultValue(0);

        // Relationships
        builder.HasMany(s => s.Cards)
               .WithOne(c => c.Sprint)
               .HasForeignKey(c => c.SprintId)
               .OnDelete(DeleteBehavior.SetNull); // Cards can exist without sprint
    }
}