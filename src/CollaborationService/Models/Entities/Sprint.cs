using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollaborationService.Models.Entities;

[Table("sprints")]
public class Sprint : BaseEntity
{
    [Key]
    public Guid SprintId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid WorkspaceId { get; set; }

    [Required]
    [MaxLength(200)]
    public string SprintName { get; set; } = string.Empty; // e.g., "Sprint 1", "Week 1-2"

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [Column(TypeName = "text")]
    public string? Goals { get; set; } // JSON array of sprint goals

    [Required]
    public DateOnly StartDate { get; set; }

    [Required]
    public DateOnly EndDate { get; set; }

    [MaxLength(50)]
    public string Status { get; set; } = "PLANNED"; // PLANNED, ACTIVE, COMPLETED, CANCELLED

    [Column(TypeName = "decimal(5,2)")]
    public decimal CompletionRate { get; set; } = 0; // 0.00 - 100.00

    public int TotalStoryPoints { get; set; } = 0;
    public int CompletedStoryPoints { get; set; } = 0;

    // Navigation properties
    [ForeignKey("WorkspaceId")]
    public virtual Workspace Workspace { get; set; } = null!;

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}