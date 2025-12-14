using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamService.Models.Entities;

[Table("teams")]
public class Team : BaseEntity
{
    [Key]
    public Guid TeamId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ClassId { get; set; } // Denormalized from AcademicService

    [Required]
    public Guid ProjectId { get; set; } // Denormalized from ProjectService

    [Required]
    [MaxLength(50)]
    public string TeamCode { get; set; } = string.Empty; // e.g., "SE1234-T01"

    [Required]
    [MaxLength(200)]
    public string TeamName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Status { get; set; } = "FORMING"; // FORMING, ACTIVE, COMPLETED, DISBANDED

    [Column(TypeName = "decimal(5,2)")]
    public decimal OverallProgress { get; set; } = 0; // 0.00 - 100.00

    public int CurrentMembers { get; set; } = 0;

    public Guid? TeamLeaderId { get; set; } // One of the TeamMembers

    public DateTime? FormedAt { get; set; }
    
    public DateTime? CompletedAt { get; set; }

    public bool IsActive { get; set; } = true;

    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    // Navigation properties
    public virtual ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
    public virtual ICollection<TeamMilestone> TeamMilestones { get; set; } = new List<TeamMilestone>();
    public virtual ICollection<Checkpoint> Checkpoints { get; set; } = new List<Checkpoint>();
    public virtual ICollection<TeamProgressLog> TeamProgressLogs { get; set; } = new List<TeamProgressLog>();
}