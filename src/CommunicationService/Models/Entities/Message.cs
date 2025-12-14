using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunicationService.Models.Entities;

[Table("messages")]
public class Message : BaseEntity, ISoftDelete
{
    [Key]
    public Guid MessageId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ChannelId { get; set; }

    [Required]
    public Guid SenderId { get; set; } // Denormalized from UserService

    [Required]
    [Column(TypeName = "text")]
    public string MessageText { get; set; } = string.Empty;

    [MaxLength(50)]
    public string MessageType { get; set; } = "TEXT"; // TEXT, FILE, IMAGE, SYSTEM

    [Column(TypeName = "text")]
    public string? Attachments { get; set; } // JSON array of file URLs

    public Guid? ParentMessageId { get; set; } // For threaded replies

    public bool IsPinned { get; set; } = false;

    public bool IsEdited { get; set; } = false;
    public DateTime? EditedAt { get; set; }

    // Soft delete
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }

    // Navigation properties
    [ForeignKey("ChannelId")]
    public virtual Channel Channel { get; set; } = null!;

    [ForeignKey("ParentMessageId")]
    public virtual Message? ParentMessage { get; set; }

    public virtual ICollection<Message> Replies { get; set; } = new List<Message>();
    public virtual ICollection<MessageReaction> MessageReactions { get; set; } = new List<MessageReaction>();
    public virtual ICollection<MessageRead> MessageReads { get; set; } = new List<MessageRead>();
}