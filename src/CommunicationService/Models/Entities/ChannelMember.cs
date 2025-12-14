using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunicationService.Models.Entities;

[Table("channel_members")]
public class ChannelMember : BaseEntity
{
    [Key]
    public Guid MemberId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ChannelId { get; set; }

    [Required]
    public Guid UserId { get; set; } // Denormalized from UserService

    [MaxLength(50)]
    public string Role { get; set; } = "MEMBER"; // OWNER, ADMIN, MEMBER

    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    public DateTime? LastReadAt { get; set; } // Last time user read messages

    public bool IsMuted { get; set; } = false;

    public bool IsActive { get; set; } = true;

    // Navigation properties
    [ForeignKey("ChannelId")]
    public virtual Channel Channel { get; set; } = null!;
}