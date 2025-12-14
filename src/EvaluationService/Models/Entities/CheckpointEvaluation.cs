using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationService.Models.Entities;

[Table("checkpoint_evaluations")]
public class CheckpointEvaluation : BaseEntity
{
    [Key]
    public Guid EvaluationId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid CheckpointId { get; set; } // Denormalized from TeamService

    [Required]
    public Guid EvaluatedBy { get; set; } // Lecturer/Instructor

    [Column(TypeName = "decimal(5,2)")]
    public decimal Score { get; set; } = 0; // 0.00 - 100.00

    [Column(TypeName = "text")]
    public string? Feedback { get; set; }

    [Column(TypeName = "text")]
    public string? Criteria { get; set; } // JSON: [{criterion: "...", score: X}]

    public bool IsApproved { get; set; } = false;

    [MaxLength(50)]
    public string Status { get; set; } = "PENDING"; // PENDING, APPROVED, REJECTED, NEEDS_REVISION

    public DateTime EvaluatedAt { get; set; } = DateTime.UtcNow;

    [Column(TypeName = "text")]
    public string? RevisionNotes { get; set; }
}