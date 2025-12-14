using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectService.Models.Entities;

[Table("project_milestones")]
public class ProjectMilestone : BaseEntity
{
    [Key]
    public Guid MilestoneId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ProjectId { get; set; }

    [Required]
    [MaxLength(50)]
    public string MilestoneCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    public string MilestoneName { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "text")]
    public string Description { get; set; } = string.Empty;

    [Column(TypeName = "text")]
    public string? Objectives { get; set; } // JSON

    [Column(TypeName = "text")]
    public string? Deliverables { get; set; } // JSON

    [Required]
    public int DurationWeeks { get; set; }

    [Required]
    public int Order { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal Weight { get; set; } = 0;

    public bool IsRequired { get; set; } = true;

    // Navigation properties
    [ForeignKey("ProjectId")]
    public virtual Project Project { get; set; } = null!;
}