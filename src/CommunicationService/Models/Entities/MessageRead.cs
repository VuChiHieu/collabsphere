using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunicationService.Models.Entities;

[Table("message_reads")]
public class MessageRead : BaseEntity
{
    [Key]
    public Guid ReadId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid MessageId { get; set; }

    [Required]
    public Guid UserId { get; set; } // Denormalized from UserService

    public DateTime ReadAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("MessageId")]
    public virtual Message Message { get; set; } = null!;
}