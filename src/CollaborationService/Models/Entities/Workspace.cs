using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollaborationService.Models.Entities;

[Table("workspaces")]
public class Workspace : BaseEntity
{
    [Key]
    public Guid WorkspaceId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid TeamId { get; set; } // Denormalized from TeamService (UNIQUE - one workspace per team)

    [Required]
    [MaxLength(200)]
    public string WorkspaceName { get; set; } = string.Empty;

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [MaxLength(50)]
    public string ViewMode { get; set; } = "KANBAN"; // KANBAN, LIST, CALENDAR, GANTT

    [Column(TypeName = "text")]
    public string? Settings { get; set; } // JSON: workspace preferences

    public bool IsActive { get; set; } = true;

    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    // Navigation properties
    public virtual ICollection<Sprint> Sprints { get; set; } = new List<Sprint>();
    public virtual ICollection<Board> Boards { get; set; } = new List<Board>();
    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();
}