using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunicationService.Models.Entities;

[Table("meeting_participants")]
public class MeetingParticipant : BaseEntity
{
    [Key]
    public Guid ParticipantId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid MeetingId { get; set; }

    [Required]
    public Guid UserId { get; set; } // Denormalized from UserService

    [MaxLength(50)]
    public string Role { get; set; } = "ATTENDEE"; // HOST, PRESENTER, ATTENDEE

    [MaxLength(50)]
    public string Status { get; set; } = "INVITED"; // INVITED, ACCEPTED, DECLINED, TENTATIVE

    public DateTime? JoinedAt { get; set; }
    public DateTime? LeftAt { get; set; }

    public int? Duration { get; set; } // Attendance duration in minutes

    public bool HasAttended { get; set; } = false;

    // Navigation properties
    [ForeignKey("MeetingId")]
    public virtual Meeting Meeting { get; set; } = null!;
}