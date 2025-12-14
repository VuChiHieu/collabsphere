using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamService.Models.Entities;

[Table("checkpoint_assignments")]
public class CheckpointAssignment : BaseEntity
{
    [Key]
    public Guid AssignmentId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid CheckpointId { get; set; }

    [Required]
    public Guid TeamMemberId { get; set; }

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

    public Guid? AssignedBy { get; set; }

    [MaxLength(50)]
    public string Status { get; set; } = "ASSIGNED"; // ASSIGNED, IN_PROGRESS, COMPLETED

    public DateTime? CompletedAt { get; set; }

    // Navigation properties
    [ForeignKey("CheckpointId")]
    public virtual Checkpoint Checkpoint { get; set; } = null!;

    [ForeignKey("TeamMemberId")]
    public virtual TeamMember TeamMember { get; set; } = null!;
}