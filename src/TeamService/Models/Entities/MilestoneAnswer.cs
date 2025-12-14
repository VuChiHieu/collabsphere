using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamService.Models.Entities;

[Table("milestone_answers")]
public class MilestoneAnswer : BaseEntity
{
    [Key]
    public Guid AnswerId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid QuestionId { get; set; }

    [Required]
    public Guid TeamMemberId { get; set; }

    [Column(TypeName = "text")]
    public string? AnswerText { get; set; }

    [MaxLength(500)]
    public string? FileUrl { get; set; } // For file upload type questions

    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

    [Column(TypeName = "decimal(5,2)")]
    public decimal? Score { get; set; } // Score received: 0.00 - MaxScore

    [Column(TypeName = "text")]
    public string? Feedback { get; set; }

    public bool IsApproved { get; set; } = false;

    public Guid? EvaluatedBy { get; set; }
    
    public DateTime? EvaluatedAt { get; set; }

    // Navigation properties
    [ForeignKey("QuestionId")]
    public virtual MilestoneQuestion MilestoneQuestion { get; set; } = null!;

    [ForeignKey("TeamMemberId")]
    public virtual TeamMember TeamMember { get; set; } = null!;
}