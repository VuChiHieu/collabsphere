using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectService.Models.Entities;

[Table("projects")]
public class Project : BaseEntity
{
    [Key]
    public Guid ProjectId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid SubjectId { get; set; } // Denormalized from AcademicService

    [Required]
    public Guid SyllabusId { get; set; } // Denormalized from AcademicService

    [Required]
    [MaxLength(50)]
    public string ProjectCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    public string ProjectName { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "text")]
    public string Description { get; set; } = string.Empty;

    [Column(TypeName = "text")]
    public string? Overview { get; set; }

    [Column(TypeName = "text")]
    public string? Requirements { get; set; } // JSON

    [Column(TypeName = "text")]
    public string? ExpectedOutcomes { get; set; } // JSON

    [Column(TypeName = "text")]
    public string? TechnicalStack { get; set; } // JSON

    [MaxLength(50)]
    public string DifficultyLevel { get; set; } = "MEDIUM"; // EASY, MEDIUM, HARD, ADVANCED

    [Required]
    public int EstimatedDuration { get; set; }

    public int MinTeamSize { get; set; } = 4;
    public int MaxTeamSize { get; set; } = 6;

    [MaxLength(50)]
    public string Status { get; set; } = "DRAFT"; // DRAFT, PENDING, APPROVED, REJECTED, ARCHIVED

    public DateTime? SubmittedAt { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public Guid? ApprovedBy { get; set; }

    [Column(TypeName = "text")]
    public string? RejectionReason { get; set; }

    public bool IsActive { get; set; } = true;

    [Required]
    public Guid CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    // Navigation properties
    public virtual ICollection<ProjectObjective> ProjectObjectives { get; set; } = new List<ProjectObjective>();
    public virtual ICollection<ProjectMilestone> ProjectMilestones { get; set; } = new List<ProjectMilestone>();
    public virtual ICollection<ClassProject> ClassProjects { get; set; } = new List<ClassProject>();
    public virtual ICollection<ProjectAIGeneration> ProjectAIGenerations { get; set; } = new List<ProjectAIGeneration>();
}