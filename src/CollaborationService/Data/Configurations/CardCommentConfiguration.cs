using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CollaborationService.Models.Entities;

namespace CollaborationService.Data.Configurations;

public class CardCommentConfiguration : IEntityTypeConfiguration<CardComment>
{
    public void Configure(EntityTypeBuilder<CardComment> builder)
    {
        builder.HasKey(cc => cc.CommentId);

        // Indexes
        builder.HasIndex(cc => cc.CardId);
        builder.HasIndex(cc => cc.UserId);
        builder.HasIndex(cc => cc.ParentCommentId);

        // Properties
        builder.Property(cc => cc.CommentText).IsRequired().HasColumnType("text");

        // Soft delete
        builder.HasQueryFilter(cc => !cc.IsDeleted);

        // Self-referencing relationship for threaded comments
        builder.HasOne(cc => cc.ParentComment)
               .WithMany(cc => cc.Replies)
               .HasForeignKey(cc => cc.ParentCommentId)
               .OnDelete(DeleteBehavior.Restrict); // Don't cascade delete replies
    }
}