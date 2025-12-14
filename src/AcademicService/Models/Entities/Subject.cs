using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicService.Models.Entities;

[Table("subjects")]
public class Subject : BaseEntity
{
    [Key]
    public Guid SubjectId { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    public string SubjectCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    public string SubjectName { get; set; } = string.Empty;

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [Required]
    public int Credits { get; set; }

    [Required]
    public int TotalHours { get; set; }

    public int? TheoryHours { get; set; }
    public int? PracticeHours { get; set; }

    public Guid? DepartmentId { get; set; }

    [Column(TypeName = "text")]
    public string? PrerequisiteSubjects { get; set; } // JSON

    public bool IsActive { get; set; } = true;

    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    // Navigation properties
    public virtual ICollection<Syllabus> Syllabi { get; set; } = new List<Syllabus>();
}