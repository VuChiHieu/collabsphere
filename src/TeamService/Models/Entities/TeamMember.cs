using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamService.Models.Entities;

[Table("team_members")]
public class TeamMember : BaseEntity
{
    [Key]
    public Guid TeamMemberId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid TeamId { get; set; }

    [Required]
    public Guid StudentId { get; set; } // Denormalized from UserService

    [MaxLength(50)]
    public string Role { get; set; } = "MEMBER"; // LEADER, MEMBER

    [Column(TypeName = "decimal(5,2)")]
    public decimal ContributionPercentage { get; set; } = 0; // 0.00 - 100.00

    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    public DateTime? LeftAt { get; set; }

    [MaxLength(50)]
    public string Status { get; set; } = "ACTIVE"; // ACTIVE, LEFT, REMOVED

    public bool IsActive { get; set; } = true;

    // Navigation properties
    [ForeignKey("TeamId")]
    public virtual Team Team { get; set; } = null!;

    public virtual ICollection<MilestoneAnswer> MilestoneAnswers { get; set; } = new List<MilestoneAnswer>();
    public virtual ICollection<CheckpointAssignment> CheckpointAssignments { get; set; } = new List<CheckpointAssignment>();
}