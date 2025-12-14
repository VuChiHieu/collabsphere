using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicService.Models.Entities;

[Table("classes")]
public class Class : BaseEntity
{
    [Key]
    public Guid ClassId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid SyllabusId { get; set; }

    [Required]
    [MaxLength(50)]
    public string ClassCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string ClassName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Semester { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string AcademicYear { get; set; } = string.Empty;

    public int MaxStudents { get; set; } = 35;
    public int CurrentStudents { get; set; } = 0;

    [Required]
    public DateOnly StartDate { get; set; }

    [Required]
    public DateOnly EndDate { get; set; }

    [Column(TypeName = "text")]
    public string? Schedule { get; set; } // JSON

    [MaxLength(50)]
    public string? Room { get; set; }

    [MaxLength(50)]
    public string Status { get; set; } = "PLANNED"; // PLANNED, ONGOING, COMPLETED, CANCELLED

    public bool IsActive { get; set; } = true;

    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    // Navigation properties
    [ForeignKey("SyllabusId")]
    public virtual Syllabus Syllabus { get; set; } = null!;

    public virtual ICollection<ClassLecturer> ClassLecturers { get; set; } = new List<ClassLecturer>();
    public virtual ICollection<ClassStudent> ClassStudents { get; set; } = new List<ClassStudent>();
    public virtual ICollection<ClassScheduleSlot> ClassScheduleSlots { get; set; } = new List<ClassScheduleSlot>();
}