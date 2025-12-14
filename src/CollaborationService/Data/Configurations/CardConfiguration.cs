using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CollaborationService.Models.Entities;

namespace CollaborationService.Data.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.HasKey(c => c.CardId);

        // Indexes
        builder.HasIndex(c => c.ColumnId);
        builder.HasIndex(c => c.SprintId);
        builder.HasIndex(c => c.Status);
        builder.HasIndex(c => c.Priority);
        builder.HasIndex(c => c.DueDate);
        builder.HasIndex(c => c.IsArchived);

        // Properties
        builder.Property(c => c.CardTitle).IsRequired().HasMaxLength(300);
        builder.Property(c => c.Description).HasColumnType("text");
        builder.Property(c => c.CardType).HasMaxLength(50).HasDefaultValue("TASK");
        builder.Property(c => c.Priority).HasMaxLength(50).HasDefaultValue("MEDIUM");
        builder.Property(c => c.Status).HasMaxLength(50).HasDefaultValue("TODO");
        builder.Property(c => c.Color).HasMaxLength(20);
        builder.Property(c => c.Tags).HasColumnType("text");
        builder.Property(c => c.Order).HasDefaultValue(0);
        builder.Property(c => c.IsArchived).HasDefaultValue(false);

        // Soft delete
        builder.HasQueryFilter(c => !c.IsDeleted);

        // Relationships
        builder.HasMany(c => c.Tasks)
               .WithOne(t => t.Card)
               .HasForeignKey(t => t.CardId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.CardAssignments)
               .WithOne(ca => ca.Card)
               .HasForeignKey(ca => ca.CardId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.CardComments)
               .WithOne(cc => cc.Card)
               .HasForeignKey(cc => cc.CardId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.CardAttachments)
               .WithOne(ca => ca.Card)
               .HasForeignKey(ca => ca.CardId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}