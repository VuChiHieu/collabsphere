using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationService.Models.Entities;

[Table("milestone_answer_peer_reviews")]
public class MilestoneAnswerPeerReview : BaseEntity
{
    [Key]
    public Guid ReviewId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid AnswerId { get; set; } // Denormalized from TeamService

    [Required]
    public Guid ReviewerId { get; set; } // Team member doing the review

    [Column(TypeName = "decimal(5,2)")]
    public decimal Rating { get; set; } = 0; // 0.00 - 5.00 (star rating)

    [Column(TypeName = "text")]
    public string? Comment { get; set; }

    public bool IsHelpful { get; set; } = false; // Did this answer help the team?

    public DateTime ReviewedAt { get; set; } = DateTime.UtcNow;
}