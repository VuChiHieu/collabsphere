using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CollaborationService.Models.Entities;

namespace CollaborationService.Data.Configurations;

public class BoardConfiguration : IEntityTypeConfiguration<Board>
{
    public void Configure(EntityTypeBuilder<Board> builder)
    {
        builder.HasKey(b => b.BoardId);

        // Indexes
        builder.HasIndex(b => b.WorkspaceId);
        builder.HasIndex(b => b.IsDefault);
        builder.HasIndex(b => b.Order);

        // Properties
        builder.Property(b => b.BoardName).IsRequired().HasMaxLength(200);
        builder.Property(b => b.Description).HasColumnType("text");
        builder.Property(b => b.Color).HasMaxLength(20);
        builder.Property(b => b.Order).HasDefaultValue(0);
        builder.Property(b => b.IsDefault).HasDefaultValue(false);
        builder.Property(b => b.IsActive).HasDefaultValue(true);

        // Relationships
        builder.HasMany(b => b.Columns)
               .WithOne(c => c.Board)
               .HasForeignKey(c => c.BoardId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}