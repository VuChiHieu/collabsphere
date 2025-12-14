using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicService.Models.Entities;

[Table("syllabi")]
public class Syllabus : BaseEntity
{
    [Key]
    public Guid SyllabusId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid SubjectId { get; set; }

    [Required]
    [MaxLength(100)]
    public string SyllabusCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    public string SyllabusName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Semester { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string AcademicYear { get; set; } = string.Empty;

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [Column(TypeName = "text")]
    public string? LearningObjectives { get; set; } // JSON

    [Column(TypeName = "text")]
    public string? CourseOutline { get; set; } // JSON

    [Column(TypeName = "text")]
    public string? AssessmentMethods { get; set; } // JSON

    [Column(TypeName = "text")]
    public string? RequiredMaterials { get; set; } // JSON

    [Column(TypeName = "text")]
    public string? TeachingMethods { get; set; }

    [Column(TypeName = "text")]
    public string? GradingScheme { get; set; } // JSON

    public int TotalSlots { get; set; } = 15;

    public bool IsActive { get; set; } = true;

    public Guid? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }

    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    // Navigation properties
    [ForeignKey("SubjectId")]
    public virtual Subject Subject { get; set; } = null!;

    public virtual ICollection<SyllabusWeek> SyllabusWeeks { get; set; } = new List<SyllabusWeek>();
    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}