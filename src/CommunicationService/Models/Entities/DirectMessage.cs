using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunicationService.Models.Entities;

[Table("direct_messages")]
public class DirectMessage : BaseEntity, ISoftDelete
{
    [Key]
    public Guid MessageId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid SenderId { get; set; } // Denormalized from UserService

    [Required]
    public Guid ReceiverId { get; set; } // Denormalized from UserService

    [Required]
    [Column(TypeName = "text")]
    public string MessageText { get; set; } = string.Empty;

    [MaxLength(50)]
    public string MessageType { get; set; } = "TEXT"; // TEXT, FILE, IMAGE

    [Column(TypeName = "text")]
    public string? Attachments { get; set; } // JSON array of file URLs

    public bool IsRead { get; set; } = false;
    public DateTime? ReadAt { get; set; }

    public bool IsEdited { get; set; } = false;
    public DateTime? EditedAt { get; set; }

    // Soft delete
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
}