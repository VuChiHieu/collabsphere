using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationService.Models.Entities;

[Table("peer_reviews")]
public class PeerReview : BaseEntity
{
    [Key]
    public Guid ReviewId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid TeamId { get; set; } // Denormalized from TeamService

    [Required]
    public Guid ReviewerId { get; set; } // Student doing the review

    [Required]
    public Guid RevieweeId { get; set; } // Student being reviewed

    [Column(TypeName = "decimal(5,2)")]
    public decimal TeamworkScore { get; set; } = 0; // 0.00 - 10.00

    [Column(TypeName = "decimal(5,2)")]
    public decimal CommunicationScore { get; set; } = 0; // 0.00 - 10.00

    [Column(TypeName = "decimal(5,2)")]
    public decimal TechnicalSkillScore { get; set; } = 0; // 0.00 - 10.00

    [Column(TypeName = "decimal(5,2)")]
    public decimal ContributionScore { get; set; } = 0; // 0.00 - 10.00

    [Column(TypeName = "decimal(5,2)")]
    public decimal OverallScore { get; set; } = 0; // Average of above scores

    [Column(TypeName = "text")]
    public string? Strengths { get; set; }

    [Column(TypeName = "text")]
    public string? AreasForImprovement { get; set; }

    [Column(TypeName = "text")]
    public string? AdditionalComments { get; set; }

    public bool IsAnonymous { get; set; } = true;

    public DateTime ReviewedAt { get; set; } = DateTime.UtcNow;

    public Guid? MilestoneId { get; set; } // Optional: review for specific milestone
}