using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunicationService.Models.Entities;

[Table("meeting_notes")]
public class MeetingNote : BaseEntity, ISoftDelete
{
    [Key]
    public Guid NoteId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid MeetingId { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string NoteContent { get; set; } = string.Empty;

    [Required]
    public Guid CreatedBy { get; set; }

    public bool IsShared { get; set; } = false; // Shared with all participants

    public DateTime? EditedAt { get; set; }

    // Soft delete
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }

    // Navigation properties
    [ForeignKey("MeetingId")]
    public virtual Meeting Meeting { get; set; } = null!;
}