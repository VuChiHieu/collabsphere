using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollaborationService.Models.Entities;

[Table("subtasks")]
public class Subtask : BaseEntity
{
    [Key]
    public Guid SubtaskId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid TaskId { get; set; }

    [Required]
    [MaxLength(300)]
    public string SubtaskTitle { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Status { get; set; } = "TODO"; // TODO, DONE

    public int Order { get; set; } = 0;

    public bool IsCompleted { get; set; } = false;

    public DateTime? CompletedAt { get; set; }

    // Navigation properties
    [ForeignKey("TaskId")]
    public virtual Task Task { get; set; } = null!;
}