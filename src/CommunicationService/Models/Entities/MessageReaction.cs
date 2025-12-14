using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunicationService.Models.Entities;

[Table("message_reactions")]
public class MessageReaction : BaseEntity
{
    [Key]
    public Guid ReactionId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid MessageId { get; set; }

    [Required]
    public Guid UserId { get; set; } // Denormalized from UserService

    [Required]
    [MaxLength(10)]
    public string Emoji { get; set; } = string.Empty; // e.g., "👍", "❤️", "😂"

    public DateTime ReactedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("MessageId")]
    public virtual Message Message { get; set; } = null!;
}