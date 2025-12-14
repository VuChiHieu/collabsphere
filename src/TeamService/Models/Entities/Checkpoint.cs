using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamService.Models.Entities;

[Table("checkpoints")]
public class Checkpoint : BaseEntity
{
    [Key]
    public Guid CheckpointId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid TeamId { get; set; }

    [Required]
    [MaxLength(200)]
    public string CheckpointName { get; set; } = string.Empty;

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [MaxLength(50)]
    public string Status { get; set; } = "PENDING"; // PENDING, IN_PROGRESS, SUBMITTED, REVIEWED, COMPLETED

    public DateOnly? DueDate { get; set; }

    public DateTime? SubmittedAt { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal? Score { get; set; } // 0.00 - 100.00

    [Column(TypeName = "text")]
    public string? Feedback { get; set; }

    public Guid? CreatedBy { get; set; }
    
    public Guid? EvaluatedBy { get; set; }
    
    public DateTime? EvaluatedAt { get; set; }

    // Navigation properties
    [ForeignKey("TeamId")]
    public virtual Team Team { get; set; } = null!;

    public virtual ICollection<CheckpointAssignment> CheckpointAssignments { get; set; } = new List<CheckpointAssignment>();
    public virtual ICollection<CheckpointSubmission> CheckpointSubmissions { get; set; } = new List<CheckpointSubmission>();
}