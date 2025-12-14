using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationService.Models.Entities;

[Table("member_evaluations")]
public class MemberEvaluation : BaseEntity
{
    [Key]
    public Guid EvaluationId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid TeamId { get; set; } // Denormalized from TeamService

    [Required]
    public Guid StudentId { get; set; } // Denormalized from UserService

    [Required]
    public Guid EvaluatedBy { get; set; } // Lecturer/Instructor

    [Column(TypeName = "decimal(5,2)")]
    public decimal Score { get; set; } = 0; // 0.00 - 100.00

    [Column(TypeName = "text")]
    public string? Feedback { get; set; }

    [Column(TypeName = "text")]
    public string? Criteria { get; set; } // JSON: [{criterion: "...", score: X}]

    [Column(TypeName = "decimal(5,2)")]
    public decimal? ContributionScore { get; set; } // Individual contribution assessment

    public DateTime EvaluatedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(50)]
    public string EvaluationType { get; set; } = "FINAL"; // MIDTERM, FINAL, CHECKPOINT

    public Guid? MilestoneId { get; set; } // Optional: evaluation for specific milestone
}