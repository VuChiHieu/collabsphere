using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollaborationService.Models.Entities;

[Table("activity_logs")]
public class ActivityLog : BaseEntity
{
    [Key]
    public Guid LogId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid WorkspaceId { get; set; }

    [Required]
    [MaxLength(50)]
    public string EntityType { get; set; } = string.Empty; // CARD, TASK, COLUMN, SPRINT

    [Required]
    public Guid EntityId { get; set; } // ID of the entity (Card/Task/etc)

    [Required]
    [MaxLength(50)]
    public string ActionType { get; set; } = string.Empty; // CREATED, UPDATED, MOVED, DELETED, ASSIGNED

    [Column(TypeName = "text")]
    public string? Changes { get; set; } // JSON: old and new values

    [Required]
    public Guid PerformedBy { get; set; } // User who performed the action

    public DateTime PerformedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("WorkspaceId")]
    public virtual Workspace Workspace { get; set; } = null!;
}