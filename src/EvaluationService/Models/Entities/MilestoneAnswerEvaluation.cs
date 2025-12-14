using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationService.Models.Entities;

[Table("milestone_answer_evaluations")]
public class MilestoneAnswerEvaluation : BaseEntity
{
    [Key]
    public Guid EvaluationId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid AnswerId { get; set; } // Denormalized from TeamService

    [Required]
    public Guid EvaluatedBy { get; set; } // Lecturer/Instructor

    [Column(TypeName = "decimal(5,2)")]
    public decimal Score { get; set; } = 0; // 0.00 - MaxScore (from question)

    [Column(TypeName = "text")]
    public string? Feedback { get; set; }

    public bool IsApproved { get; set; } = false;

    [MaxLength(50)]
    public string Status { get; set; } = "PENDING"; // PENDING, APPROVED, REJECTED, NEEDS_REVISION

    public DateTime EvaluatedAt { get; set; } = DateTime.UtcNow;

    [Column(TypeName = "text")]
    public string? RevisionNotes { get; set; }
}