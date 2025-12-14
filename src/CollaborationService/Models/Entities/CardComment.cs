using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollaborationService.Models.Entities;

[Table("card_comments")]
public class CardComment : BaseEntity, ISoftDelete
{
    [Key]
    public Guid CommentId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid CardId { get; set; }

    [Required]
    public Guid UserId { get; set; } // Denormalized from UserService

    [Required]
    [Column(TypeName = "text")]
    public string CommentText { get; set; } = string.Empty;

    public Guid? ParentCommentId { get; set; } // For threaded comments (replies)

    public DateTime? EditedAt { get; set; }

    // Soft delete
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }

    // Navigation properties
    [ForeignKey("CardId")]
    public virtual Card Card { get; set; } = null!;

    [ForeignKey("ParentCommentId")]
    public virtual CardComment? ParentComment { get; set; }

    public virtual ICollection<CardComment> Replies { get; set; } = new List<CardComment>();
}