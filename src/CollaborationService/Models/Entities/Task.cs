using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollaborationService.Models.Entities;

[Table("tasks")]
public class Task : BaseEntity
{
    [Key]
    public Guid TaskId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid CardId { get; set; }

    [Required]
    [MaxLength(300)]
    public string TaskTitle { get; set; } = string.Empty;

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [MaxLength(50)]
    public string Status { get; set; } = "TODO"; // TODO, IN_PROGRESS, DONE

    public int? EstimatedHours { get; set; }
    public int? ActualHours { get; set; }

    public DateOnly? DueDate { get; set; }

    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    public int Order { get; set; } = 0;

    public bool IsCompleted { get; set; } = false;

    // Navigation properties
    [ForeignKey("CardId")]
    public virtual Card Card { get; set; } = null!;

    public virtual ICollection<Subtask> Subtasks { get; set; } = new List<Subtask>();
    
    public virtual ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();
}