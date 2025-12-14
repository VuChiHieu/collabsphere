using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationService.Models.Entities;

[Table("team_evaluations")]
public class TeamEvaluation : BaseEntity
{
    [Key]
    public Guid EvaluationId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid TeamId { get; set; } // Denormalized from TeamService

    [Required]
    public Guid EvaluatedBy { get; set; } // Lecturer/Instructor

    [Column(TypeName = "decimal(5,2)")]
    public decimal OverallScore { get; set; } = 0; // 0.00 - 100.00

    [Column(TypeName = "text")]
    public string? Feedback { get; set; }

    [Column(TypeName = "text")]
    public string? Criteria { get; set; } // JSON: [{criterion: "...", score: X}]

    public DateTime EvaluatedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(50)]
    public string EvaluationType { get; set; } = "FINAL"; // MIDTERM, FINAL, CHECKPOINT

    public Guid? MilestoneId { get; set; } // Optional: evaluation for specific milestone
}