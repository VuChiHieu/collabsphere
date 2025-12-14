using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunicationService.Models.Entities;

[Table("meetings")]
public class Meeting : BaseEntity
{
    [Key]
    public Guid MeetingId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid TeamId { get; set; } // Denormalized from TeamService

    public Guid? ChannelId { get; set; } // Optional: meeting can be linked to a channel

    [Required]
    [MaxLength(300)]
    public string MeetingTitle { get; set; } = string.Empty;

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [MaxLength(50)]
    public string MeetingType { get; set; } = "VIDEO"; // VIDEO, AUDIO, SCREEN_SHARE

    [MaxLength(50)]
    public string Status { get; set; } = "SCHEDULED"; // SCHEDULED, IN_PROGRESS, COMPLETED, CANCELLED

    [MaxLength(500)]
    public string? MeetingUrl { get; set; } // External meeting link (Zoom, Teams, etc.)

    public DateTime? ScheduledStartTime { get; set; }
    public DateTime? ScheduledEndTime { get; set; }
    
    public DateTime? ActualStartTime { get; set; }
    public DateTime? ActualEndTime { get; set; }

    public int? Duration { get; set; } // Duration in minutes

    [Column(TypeName = "text")]
    public string? Agenda { get; set; } // Meeting agenda

    [MaxLength(500)]
    public string? RecordingUrl { get; set; }

    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    // Navigation properties
    [ForeignKey("ChannelId")]
    public virtual Channel? Channel { get; set; }

    public virtual ICollection<MeetingParticipant> MeetingParticipants { get; set; } = new List<MeetingParticipant>();
    public virtual ICollection<MeetingNote> MeetingNotes { get; set; } = new List<MeetingNote>();
}