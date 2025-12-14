using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunicationService.Models.Entities;

[Table("channels")]
public class Channel : BaseEntity, ISoftDelete
{
    [Key]
    public Guid ChannelId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid TeamId { get; set; } // Denormalized from TeamService

    [Required]
    [MaxLength(200)]
    public string ChannelName { get; set; } = string.Empty;

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [MaxLength(50)]
    public string ChannelType { get; set; } = "PUBLIC"; // PUBLIC, PRIVATE, DIRECT

    public bool IsPrivate { get; set; } = false;

    public bool IsDefault { get; set; } = false; // Default channel for team (e.g., "General")

    public int MemberCount { get; set; } = 0;

    [MaxLength(500)]
    public string? AvatarUrl { get; set; }

    // Soft delete
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }

    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    // Navigation properties
    public virtual ICollection<ChannelMember> ChannelMembers { get; set; } = new List<ChannelMember>();
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    public virtual ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();
}