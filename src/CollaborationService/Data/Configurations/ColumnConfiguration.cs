using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CollaborationService.Models.Entities;

namespace CollaborationService.Data.Configurations;

public class ColumnConfiguration : IEntityTypeConfiguration<Column>
{
    public void Configure(EntityTypeBuilder<Column> builder)
    {
        builder.HasKey(c => c.ColumnId);

        // Indexes
        builder.HasIndex(c => c.BoardId);
        builder.HasIndex(c => c.Order);
        builder.HasIndex(c => c.ColumnType);

        // Properties
        builder.Property(c => c.ColumnName).IsRequired().HasMaxLength(100);
        builder.Property(c => c.ColumnType).HasMaxLength(50).HasDefaultValue("CUSTOM");
        builder.Property(c => c.Color).HasMaxLength(20);
        builder.Property(c => c.WipLimit).HasDefaultValue(0);
        builder.Property(c => c.Order).IsRequired();
        builder.Property(c => c.IsActive).HasDefaultValue(true);

        // Relationships
        builder.HasMany(c => c.Cards)
               .WithOne(card => card.Column)
               .HasForeignKey(card => card.ColumnId)
               .OnDelete(DeleteBehavior.Restrict); // Don't delete column if has cards
    }
}