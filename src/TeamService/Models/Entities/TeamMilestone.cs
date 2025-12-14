using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamService.Models.Entities;

[Table("team_milestones")]
public class TeamMilestone : BaseEntity
{
    [Key]
    public Guid TeamMilestoneId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid TeamId { get; set; }

    [Required]
    public Guid ProjectMilestoneId { get; set; } // Denormalized from ProjectService

    [MaxLength(50)]
    public string Status { get; set; } = "NOT_STARTED"; // NOT_STARTED, IN_PROGRESS, UNDER_REVIEW, COMPLETED

    [Column(TypeName = "decimal(5,2)")]
    public decimal Progress { get; set; } = 0; // 0.00 - 100.00

    public DateOnly? StartDate { get; set; }
    
    public DateOnly? EndDate { get; set; }
    
    public DateOnly? DueDate { get; set; }
    
    public DateTime? SubmittedAt { get; set; }
    
    public DateTime? CompletedAt { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal? Score { get; set; } // Final score: 0.00 - 100.00

    [Column(TypeName = "text")]
    public string? Feedback { get; set; }

    // Navigation properties
    [ForeignKey("TeamId")]
    public virtual Team Team { get; set; } = null!;

    public virtual ICollection<MilestoneQuestion> MilestoneQuestions { get; set; } = new List<MilestoneQuestion>();
}